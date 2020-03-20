using System;
using CoderaShopping.Business.Models;
using CoderaShopping.Domain;

namespace CoderaShopping.Business.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel MapToViewModel(this User user)
        {
            var model = new UserViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Password = user.Password;
            model.Email = user.Email;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.City = user.City;
            model.Country = user.Country;
            model.Address = user.Address;
            model.PostalCode = user.PostalCode;
            model.Role = user.Role;
            return model;
        }
        public static UserGridViewModel MapToGridViewModel(this User user)
        {
            var model = new UserGridViewModel();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.Email = user.Email;
            model.Name = user.Name;
            model.Surname = user.Surname;
            model.Role = user.Role;
            return model;
        }

    }
}
