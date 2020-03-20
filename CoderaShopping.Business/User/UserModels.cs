using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using CoderaShopping.Domain;
using FluentValidation;
using FluentValidation.Attributes;
using FluentValidation.Internal;

namespace CoderaShopping.Business.Models
{
    #region Validators
    public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
    {
        
        public RegisterUserViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(30);
            RuleFor(x => x.Email).
                NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.ConfirmedPassword)
                .NotEmpty().WithMessage("Password confirmation is required")
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(30);
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(30);
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(30);
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50);
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(100);
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .Matches("[0-9]{4}").WithMessage("Invalid postal code format.");
        }
    }
    public class UserGridViewModelValidator : AbstractValidator<UserGridViewModel>
    {
        public UserGridViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Email).
                NotEmpty().
                EmailAddress();
            
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Role)
                .IsInEnum()
                .NotEmpty();
        }
    }
    public class UserViewModelValidator : AbstractValidator<UserViewModel>
    {
        public UserViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Username is required")
                .MaximumLength(30);
            RuleFor(x => x.Email).
                NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(30);
            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Surname is required")
                .MaximumLength(30);
            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required")
                .MaximumLength(30);
            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50);
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required")
                .MaximumLength(100);
            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("Postal code is required")
                .Matches("[0-9]{4}").WithMessage("Invalid postal code format.");
            RuleFor(x => x.Role)
                .IsInEnum()
                .NotEmpty();
        }
    }
    public class UpdateUserViewModelValidator : AbstractValidator<UpdateUserViewModel>
    {
        public UpdateUserViewModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.UserName)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Email).
                NotEmpty().
                EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.Surname)
                .NotEmpty()
                .MaximumLength(30);
            RuleFor(x => x.City)
                .NotEmpty().
                MaximumLength(30);
            RuleFor(x => x.Country)
                .NotEmpty()
                .MaximumLength(50);
            RuleFor(x => x.Address)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .Matches("[0-9]{4}");
        }
    }
    public class SignInUserViewModelValidator : AbstractValidator<SignInUserViewModel>
    {
        public SignInUserViewModelValidator()
        {
            RuleFor(x => x.Email).
                NotEmpty().
                EmailAddress();
            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .MaximumLength(20);
        }
    }
    #endregion
    #region ViewModels

    [Validator(typeof(UserGridViewModelValidator))]
    public class UserGridViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
    }

    [Validator(typeof(RegisterUserViewModelValidator))]
    public class RegisterUserViewModel {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ConfirmedPassword { get; set; }
        public string Password { get; set; }
        public string  Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }

    public class UpdateUserViewModel {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

    }
    [Validator(typeof(UserViewModelValidator))]

    public class UserViewModel : RegisterUserViewModel {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
    }
    [Validator(typeof(SignInUserViewModelValidator))]

    public class SignInUserViewModel {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    #endregion
}
