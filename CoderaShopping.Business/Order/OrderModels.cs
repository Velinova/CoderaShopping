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
    [Validator(typeof(OrderViewModelValidator))]
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }

        public OrderStatus Status { get; set; }
        public DateTime DateOrdered { get; set; }
        public DateTime DateDelivered { get; set; }
        public int Price { get; set; }
    }
    public class UpdateOrderViewModel
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateDelivered { get; set; }

    }
    public class GridOrderViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }

        public String Status { get; set; }
        public String DateOrdered { get; set; }
        public int Price { get; set; }
    }
    public class OrderWithItemsViewModel
    {
        public Guid Id { get; set; }
        public UserViewModel User { get; set; }

        public String Status { get; set; }
        public String DateOrdered { get; set; }
        public int Price { get; set; }
        public List<OrderItemViewModel> Items { get; set; }
    }
    #region Validators
    public class OrderViewModelValidator : AbstractValidator<OrderViewModel>
    {
        public OrderViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Status)
                .NotEmpty()
                .IsInEnum();
            RuleFor(x => x.DateOrdered)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
    #endregion
}
