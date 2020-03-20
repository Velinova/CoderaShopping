using System;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class OrderItemMapper
    {
        public static OrderItemViewModel MapToViewModel(this OrderItem item)
        {
            var viewModel = new OrderItemViewModel();
            viewModel.Id = item.Id;

            viewModel.Order = new OrderViewModel
            {
                Id = item.Order.Id,
                User = item.Order.User.MapToViewModel(),
                DateOrdered = item.Order.DateOrdered,
                Status = item.Order.Status,
                Price = item.Order.OrderPrice
            };
            viewModel.Product = new ProductViewModel {
                Id = item.Product.Id,
                Name = item.Product.Name,
                Description = item.Product.Description,
                Price = item.Product.Price,
                Quantity = item.Product.Quantity,
                Category = item.Product.Category.MapToViewModel()
            };
            viewModel.Quantity = item.Quantity;
            return viewModel;
        }
    }
}
