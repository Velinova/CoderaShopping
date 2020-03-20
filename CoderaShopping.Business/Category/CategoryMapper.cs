using System;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryViewModel MapToViewModel(this Category category)
        {
            var viewModel = new CategoryViewModel();
            viewModel.Id = category.Id;
            viewModel.Name = category.Name;
            viewModel.Status = category.Status;
            viewModel.IsDefault = category.IsDefault;
            return viewModel;
        }

        public static CategoryGridModel MapToGridModel(this Category category)
        {
            var gridModel = new CategoryGridModel();
            gridModel.Id = category.Id;
            gridModel.Name = category.Name;
            gridModel.Status = category.Status;
            gridModel.IsDefault = category.IsDefault;
            gridModel.Count = category.Products.Count;
            return gridModel;
        }

        public static LookupViewModel MapToLookupViewModel(this Category category)
        {
            return new LookupViewModel{
                Id = category.Id,
                Name = category.Name,
                Status = category.Status
            };
        }
    }
}
