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
    //[Validator(typeof(OrderViewModelValidator))]
    public class ManifacturerViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }

       
    }
   
    public class CreateManifacturerViewModel
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }


    }

    #region Validators
    public class ManifacturerViewModelValidator : AbstractValidator<ManifacturerViewModel>
    {
        public ManifacturerViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.City)
                .NotEmpty();
            RuleFor(x => x.Country)
                .NotEmpty();
            RuleFor(x => x.Address)
                .NotEmpty();
        }
    }
    #endregion
}
