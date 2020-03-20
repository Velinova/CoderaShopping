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
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public CategoryViewModel Category { get; set; }
        public ManifacturerViewModel Manifacturer { get; set; }

    }

    [Validator(typeof(CreateProductViewModelValidator))]
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public CategoryViewModel Category { get; set; }
        public ManifacturerViewModel Manifacturer { get; set; }

    }
    [Validator(typeof(UpdateProductViewModelValidator))]
    public class UpdateProductViewModel : CreateProductViewModel
    {
        public Guid Id { get; set; }

    }

    #region Validators
    public class CreateProductViewModelValidator : AbstractValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(300);
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
    public class UpdateProductViewModelValidator : AbstractValidator<UpdateProductViewModel>
    {
        public UpdateProductViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(300);
            RuleFor(x => x.Quantity)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
            
        }
    }
    #endregion
}
