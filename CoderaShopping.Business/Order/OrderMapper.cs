using System;
using System.Collections.Generic;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;
namespace CoderaShopping.Business.Mappers
{
    public static class OrderMapper
    {
        public static OrderViewModel MapToViewModel(this Order order)
        {
            var viewModel = new OrderViewModel();
            viewModel.Id = order.Id;
            viewModel.Status = order.Status;
            viewModel.Price = order.OrderPrice;
            viewModel.DateOrdered = order.DateOrdered;
            viewModel.DateDelivered = order.DateDelivered;
            viewModel.User = new UserViewModel
            {
                Id = order.User.Id,
                Role = order.User.Role,
                UserName = order.User.UserName,
                Email = order.User.Email,
                ConfirmedPassword = order.User.Password,
                Password = order.User.Password,
                Name = order.User.Name,
                Surname = order.User.Surname,
                City = order.User.City,
                Country = order.User.Country,
                Address = order.User.Address,
                PostalCode = order.User.PostalCode
            };
            return viewModel;
        }
        public static OrderWithItemsViewModel MapToOrderItemsViewModel(this Order order)
        {
            var viewModel = new OrderWithItemsViewModel();
            viewModel.Id = order.Id;
            switch ((int)order.Status)
            {
                case 0:
                    viewModel.Status = "Paid";
                    break;
                case 1:
                    viewModel.Status = "Shipped";
                    break;
                case 2:
                    viewModel.Status = "Delivered";
                    break;
                case 3:
                    viewModel.Status = "Frozen";
                    break;
                case 4:
                    viewModel.Status = "Canceled";
                    break;
                case 5:
                    viewModel.Status = "Disputed";
                    break;
            }
            viewModel.DateOrdered = order.DateOrdered.ToString();
            viewModel.Price = order.OrderPrice;
            viewModel.User = new UserViewModel
            {
                Id = order.User.Id,
                Role = order.User.Role,
                UserName = order.User.UserName,
                Email = order.User.Email,
                ConfirmedPassword = order.User.Password,
                Password = order.User.Password,
                Name = order.User.Name,
                Surname = order.User.Surname,
                City = order.User.City,
                Country = order.User.Country,
                Address = order.User.Address,
                PostalCode = order.User.PostalCode
            };
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();
            foreach(var item in order.OrderItems)
            {
                items.Add(item.MapToViewModel());
            }
            viewModel.Items = items;
            return viewModel;
        }
        public static GridOrderViewModel MapToGridViewModel(this Order order)
        {
            var viewModel = new GridOrderViewModel();
            viewModel.Id = order.Id;
            viewModel.Price = order.OrderPrice;
            switch ((int)order.Status) {
                case 0:
                    viewModel.Status = "Paid";
                    break;
                case 1:
                    viewModel.Status = "Shipped";
                    break;
                case 2:
                    viewModel.Status = "Delivered";
                    break;
                case 3:
                    viewModel.Status = "Frozen";
                    break;
                case 4:
                    viewModel.Status = "Canceled";
                    break;
                case 5:
                    viewModel.Status = "Disputed";
                    break;
            }
            viewModel.DateOrdered = order.DateOrdered.ToString();
            viewModel.User = new UserViewModel
            {
                Id = order.User.Id,
                Role = order.User.Role,
                UserName = order.User.UserName,
                Email = order.User.Email,
                ConfirmedPassword = order.User.Password,
                Password = order.User.Password,
                Name = order.User.Name,
                Surname = order.User.Surname,
                City = order.User.City,
                Country = order.User.Country,
                Address = order.User.Address,
                PostalCode = order.User.PostalCode
            };
            return viewModel;
        }
    }
}
