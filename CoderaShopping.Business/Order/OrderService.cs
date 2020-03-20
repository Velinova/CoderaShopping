using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.Core;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IOrderService
    {
        #region CRUD
        OrderViewModel GetById(Guid id);
        Guid Create(OrderViewModel model);
        void Update(UpdateOrderViewModel model);
        void Delete(Guid id);
        #endregion

        IList<GridOrderViewModel> GetAll();
        void Pay(List<OrderItemAddToOrderViewModel> model, Guid userId);
        IList<OrderItemViewModel> GetOrderItems(Guid id);
        IList<OrderWithItemsViewModel> GetOrdersAndItemsByUser(Guid userId, SearchParameter parameter);
        int GetOrdersCountByMonth(int month);
    }

    public class OrderService : IOrderService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository,IProductRepository productRepository, IOrderItemRepository orderItemRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }
        
       
        public Guid Create(OrderViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var user = _userRepository.GetById(model.User.Id);
            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            var order = new Order(user, model.Status, model.DateOrdered, model.Price);
            _orderRepository.Add(order);
            _unitOfWork.Commit();
            return order.Id;
        }
        public void Update(UpdateOrderViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var order = _orderRepository.GetById(model.OrderId); 

            if (order == null) {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }

            var user = _userRepository.GetById(model.UserId);
            if(user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }
            order.User = user;
            order.Status = Order.MapToOrderStatus(Convert.ToInt32(model.Status));
            //order.DateOrdered = Convert.ToDateTime(model.DateOrdered);
            //order.OrderPrice = model.Price;

            _orderRepository.Update(order);
            _unitOfWork.Commit();
            
        }
        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var order = _orderRepository.GetById(id);
            if(order == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }
            _orderRepository.Delete(order);
            _unitOfWork.Commit();
        }
        public OrderViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }

            var orderViewModel = order.MapToViewModel();
            _unitOfWork.Commit();

            return orderViewModel;

        }

        public IList<GridOrderViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var orders = _orderRepository.GetAll();
            var ordersViewModel = orders.Select(x => x.MapToGridViewModel()).ToList();

            _unitOfWork.Commit();
            return ordersViewModel;
        }

        public void Pay(List<OrderItemAddToOrderViewModel> model, Guid userId)
        {
            _unitOfWork.BeginTransaction();
            //sum price
            int orderPrice = 0;
            foreach(var item in model)
            {
                int quantity = item.Quantity;
                int price = (int) item.Product.Price;
                orderPrice += price * quantity;
            }

            //get user from cookie
            var user = _userRepository.GetById(userId);

            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            //create new empty order
            var order = new Order(user, OrderStatus.PAID, DateTime.Now, orderPrice);

            _orderRepository.Add(order);

            //add order items in created order
            foreach(var item in model)
            {
                var product = _productRepository.GetById(item.Product.Id);

                if(product == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
                }

                var orderItem = new OrderItem(order, product, item.Quantity);

                if(orderItem.Quantity > product.Quantity)
                {
                    _unitOfWork.Rollback();
                    throw new Exception(ExceptionMessages.OrderItemException.OUT_OF_STOCK);

                }
                _orderItemRepository.Add(orderItem);

                product.Quantity = product.Quantity - orderItem.Quantity;
                _productRepository.Update(product);
            }
            _unitOfWork.Commit();

        }

        public IList<OrderItemViewModel> GetOrderItems(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var order = _orderRepository.GetById(id);

            if(order == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
            }

            var orderItems = _orderRepository.GetAllOrderItems(order);
            var orderItemsViewModel = orderItems.Select(x => x.MapToViewModel()).ToList();

            _unitOfWork.Commit();

            return orderItemsViewModel;
        }

        public IList<OrderWithItemsViewModel> GetOrdersAndItemsByUser(Guid userId, SearchParameter parameter)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(userId);

            if(user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            var orders = _orderRepository.GetOrdersAndItemsByUser(user, parameter);
            var ordersViewModel = orders.Select(x => x.MapToOrderItemsViewModel()).ToList();

            _unitOfWork.Commit();
            return ordersViewModel;
        }

        public int GetOrdersCountByMonth(int month)
        {
            _unitOfWork.BeginTransaction();
            var orders = _orderRepository.GetAll();
            var count = orders.Where(x => x.DateOrdered.Month == month).ToList().Count;
            _unitOfWork.Commit();
            return count;
        }
    }
}
