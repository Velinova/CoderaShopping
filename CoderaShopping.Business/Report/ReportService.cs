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
    public interface IReportService
    {
        IList<TopManufacturersViewModel> GetTopManufacturers();
        IList<TopCustomersViewModel> GetTopCustomers();
        OrdersByStatusLastSixMonths ReportOrdersByStatusLastSixMonths(int[] months);
    }

    public class ReportService : IReportService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IManifacturerRepository _manifacturerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUserRepository userRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IManifacturerRepository manifacturerRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _manifacturerRepository = manifacturerRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public IList<TopManufacturersViewModel> GetTopManufacturers()
        {
            _unitOfWork.BeginTransaction();
            var countedItems = new List<TopManufacturersViewModel>();
            var query = _orderRepository.GetAll().SelectMany(x => x.OrderItems)
                                                  .GroupBy(x => x.Product.Manifacturer.Name)
                                                  .Take(5).ToList();

            foreach (var item in query)
            {
                var count = query.SelectMany(x => x).Where(x => x.Product.Manifacturer.Name == item.Key).Count();
                countedItems.Add(new TopManufacturersViewModel
                {
                    Name = item.Key,
                    Count = count
                });
            }

            _unitOfWork.Commit();
            return countedItems;
        }
        public IList<TopCustomersViewModel> GetTopCustomers()
        {
            _unitOfWork.BeginTransaction();
            var result = new List<TopCustomersViewModel>();
            var query = _userRepository.GetAll().Select(x => x).OrderByDescending(x => x.Orders.ToList().Count).Take(5).ToList();
            foreach(var item in query)
            {
                result.Add(new TopCustomersViewModel
                {
                    Name = item.UserName,
                    Count = item.Orders.Count
                });
            }
            return result;
        }
        public OrdersByStatusLastSixMonths ReportOrdersByStatusLastSixMonths(int [] months)
        {
            _unitOfWork.BeginTransaction();
            var model = new OrdersByStatusLastSixMonths();
            foreach(var month in months)
            {
                var paid = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.PAID) && x.DateOrdered.Month == month).ToList().Count;
                var shipped = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.SHIPPED) && x.DateOrdered.Month == month).ToList().Count;
                var delivered = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.DELIVERED) && x.DateOrdered.Month == month).ToList().Count;
                var canceled = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.CANCELED) && x.DateOrdered.Month == month).ToList().Count;
                var frozen = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.FROZEN) && x.DateOrdered.Month == month).ToList().Count;
                var disputed = _orderRepository.GetAll().Where(x => x.Status.Equals(OrderStatus.DISPUTED) && x.DateOrdered.Month == month).ToList().Count;
                List<int> counts = new List<int>() {
                    delivered,
                    shipped,
                    paid,
                    canceled,
                    frozen,
                    disputed
                };
                model.Dictionary.Add(month, counts);
            }
            _unitOfWork.Commit();
            return model;
        }
    }
}
