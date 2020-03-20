using FluentValidation;
using FluentValidation.Attributes;
using System;

namespace CoderaShopping.Business.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
    }
    public class CategoryGridModel : CategoryViewModel
    {
        public int Count { get; set; }
    }
   
    [Validator(typeof(UpdateCategoryViewModelValidator))]
    public class UpdateCategoryViewModel : CreateCategoryViewModel
    {
        public Guid Id { get; set; }
    }

    [Validator(typeof(CreateCategoryViewModelValidator))]
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }
        public bool Status { get; set; }
        public bool IsDefault { get; set; }
    }


    #region Validators
    public class UpdateCategoryViewModelValidator : AbstractValidator<UpdateCategoryViewModel>
    {
        public UpdateCategoryViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
    public class CreateCategoryViewModelValidator : AbstractValidator<CreateCategoryViewModel>
    {
        public CreateCategoryViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
    #endregion
}
