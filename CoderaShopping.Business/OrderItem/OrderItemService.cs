using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IOrderItemService
    {
        #region CRUD
        OrderItemViewModel GetById(Guid id);
        Guid Create(OrderItemViewModel model);
        void Update(OrderItemViewModel model);
        void Delete(Guid id);
        #endregion

        IList<OrderItemViewModel> GetAll();
    }

    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderItemService(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public Guid Create(OrderItemViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var order = _orderRepository.GetById(model.Order.Id);
            if (order == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }

            var product = _productRepository.GetById(model.Product.Id);
            if (product == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
            }

            var orderItem = new OrderItem(order, product, model.Quantity);

            _orderItemRepository.Add(orderItem);
            _unitOfWork.Commit();
            return orderItem.Id;
        }
        public void Update(OrderItemViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var orderItem = _orderItemRepository.GetById(model.Id);

            if (orderItem == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
            }

            var order = _orderRepository.GetById(model.Order.Id);
            if (order == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }
            var product = _productRepository.GetById(model.Product.Id);
            if (product == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
            }


            orderItem.Order = order;
            orderItem.Product = product;
            orderItem.Quantity = model.Quantity;

            _orderItemRepository.Update(orderItem);
            _unitOfWork.Commit();

        }
        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var orderItem = _orderItemRepository.GetById(id);
            if (orderItem == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
            }
            _orderItemRepository.Delete(orderItem);
            _unitOfWork.Commit();
        }
        public OrderItemViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var orderItem = _orderItemRepository.GetById(id);

            if (orderItem == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
            }

            var orderItemViewModel = orderItem.MapToViewModel();
            _unitOfWork.Commit();

            return orderItemViewModel;

        }

        public IList<OrderItemViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var orderItem = _orderItemRepository.GetAll();
            var orderItemViewModel = orderItem.Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return orderItemViewModel;
        }
    }
}
