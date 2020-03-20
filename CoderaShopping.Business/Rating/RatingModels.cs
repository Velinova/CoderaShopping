using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CoderaShopping.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;

namespace CoderaShopping.Business.Models
{

    [Validator(typeof(RatingViewModelValidator))]
    public class RatingViewModel
    {
        public Guid  Id { get; set; }
        public UserViewModel User { get; set; }
        public Guid ObjectId { get; set; }
        public RatingObjectType ObjectType { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public bool ShowName { get; set; }
    }
    public class CreateUpdateRatingViewModel
    {
        public Guid  Id { get; set; }

        public Guid UserId { get; set; }
        public Guid ObjectId { get; set; }
        public RatingObjectType ObjectType { get; set; }
        public int Value { get; set; }
        public string Comment { get; set; }
        public bool ShowName { get; set; }
    }
    public class RatingValuesViewModel
    {
        public int Total { get; set; }
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four {get; set;}
        public int Five { get; set; }
    }

    #region Validators

    public class RatingViewModelValidator : AbstractValidator<RatingViewModel>
    {
        public RatingViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.ObjectId)
                .NotEmpty();
            RuleFor(x => x.ShowName)
                .NotEmpty();
            RuleFor(x => x.Value)
                .NotEmpty()
                .InclusiveBetween(1, 5);
        }
    }
    

    #endregion
}
