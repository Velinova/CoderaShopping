using System;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel MapToViewModel(this Product product)
        {
            var viewModel = new ProductViewModel();
            viewModel.Id = product.Id;
            viewModel.Name = product.Name;
            viewModel.Description = product.Description;
            viewModel.Price = product.Price;
            viewModel.Quantity = product.Quantity;
            viewModel.Category = new CategoryViewModel
            {
             Id = product.Category.Id,
             Name = product.Category.Name,
             Status = product.Category.Status,
             IsDefault = product.Category.IsDefault
            };
            viewModel.Manifacturer = new ManifacturerViewModel
            {
                Id = product.Manifacturer.Id,
                Name = product.Manifacturer.Name,
                City = product.Manifacturer.City,
                Country = product.Manifacturer.Country,
                Address = product.Manifacturer.Address
            };
            return viewModel;
        }
    }
}
