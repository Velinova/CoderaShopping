using System;
using System.Collections.Generic;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;
namespace CoderaShopping.Business.Mappers
{
    public static class ManifacturerMapper
    {
        public static ManifacturerViewModel MapToViewModel(this Manifacturer manifacturer)
        {
            var viewModel = new ManifacturerViewModel();
            viewModel.Id = manifacturer.Id;
            viewModel.Name = manifacturer.Name;
            viewModel.City = manifacturer.City;
            viewModel.Country = manifacturer.Country;
            viewModel.Address = manifacturer.Address;

            return viewModel;
        }
        
    }
}
