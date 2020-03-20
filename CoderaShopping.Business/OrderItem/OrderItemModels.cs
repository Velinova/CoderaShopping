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
   
    [Validator(typeof(OrderItemViewModelValidator))]
    public class OrderItemViewModel
    {
        public Guid Id { get; set; }
        public OrderViewModel Order { get; set; }
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
    [Validator(typeof(OrderItemAddToViewModelValidator))]
    public class OrderItemAddToOrderViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Quantity { get; set; }
    }
   
    #region Validators

    public class OrderItemViewModelValidator : AbstractValidator<OrderItemViewModel>
    {
        public OrderItemViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Order)
                .NotEmpty();
            RuleFor(x => x.Product)
                .NotEmpty();
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
    public class OrderItemAddToViewModelValidator : AbstractValidator<OrderItemAddToOrderViewModel>
    {
        public OrderItemAddToViewModelValidator()
        {
            RuleFor(x => x.Product)
                .NotEmpty();
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
   
    #endregion
}
