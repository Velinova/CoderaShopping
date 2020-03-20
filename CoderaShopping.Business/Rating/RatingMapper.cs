using System;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class RatingMapper
    {
        public static RatingViewModel MapToViewModel(this Rating rating)
        {
            var viewModel = new RatingViewModel();
            viewModel.Id = rating.Id;

            viewModel.User = rating.User.MapToViewModel();
            viewModel.ObjectId = rating.ObjectId;
            viewModel.ObjectType = rating.ObjectType;
            viewModel.Value = rating.Value;
            viewModel.Comment = rating.Comment;
            viewModel.ShowName = rating.ShowName;
            return viewModel;
        }
        public static CreateUpdateRatingViewModel MapToCreateUpdateViewModel(this Rating rating)
        {
            var viewModel = new CreateUpdateRatingViewModel();
            viewModel.Id = rating.Id;

            viewModel.UserId = rating.User.Id;
            viewModel.ObjectId = rating.ObjectId;
            viewModel.ObjectType = rating.ObjectType;
            viewModel.Value = rating.Value;
            viewModel.Comment = rating.Comment;
            viewModel.ShowName = rating.ShowName;
            return viewModel;
        }
    }
}
