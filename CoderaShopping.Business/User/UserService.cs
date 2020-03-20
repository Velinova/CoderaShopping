using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IUserService
    {
        UserViewModel GetById(Guid id);
        void Update(UpdateUserViewModel model);
        void Delete(Guid id);
        void ChangeRole(Guid id);
        UserRole GetRole(Guid id);
        Guid RegisterUser(RegisterUserViewModel model);
        Guid SignInUser(SignInUserViewModel model);
        IList<UserGridViewModel> GetAllAdmins();
        IList<UserGridViewModel> GetAllUsers();
        IList<UserGridViewModel> GetAll();
        IList<GridOrderViewModel> GetOrdersById(Guid id);

    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUserRepository userRepository, IOrderRepository  orderRepository,  IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _ordersRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public void ChangeRole(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            if (user.Role == UserRole.EXTERNAL)
            {
                user.Role = UserRole.INTERNAL;
            }
            else
            {
                user.Role = UserRole.EXTERNAL;
            }

            _userRepository.Update(user);
            _unitOfWork.Commit();
        }

        public void Delete(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            _userRepository.Delete(user);
            _unitOfWork.Commit();
        }

        public IList<UserGridViewModel> GetAll()
        {
            _unitOfWork.BeginTransaction();

            var allUsers = _userRepository.getAll();
            var allUsersViewModel = allUsers.Select(x => x.MapToGridViewModel()).ToList();

            _unitOfWork.Commit();

            return allUsersViewModel;
        }

        public IList<UserGridViewModel> GetAllAdmins()
        {
            _unitOfWork.BeginTransaction();

            var admins = _userRepository.getAllAdmins();
            var adminsViewModel = admins.Select(x => x.MapToGridViewModel()).ToList();

            _unitOfWork.Commit();

            return adminsViewModel;
        }

        public IList<UserGridViewModel> GetAllUsers()
        {
            _unitOfWork.BeginTransaction();

            var users = _userRepository.getAllUsers();
            var usersViewModel = users.Select(x => x.MapToGridViewModel()).ToList();

            _unitOfWork.Commit();

            return usersViewModel;
        }

        public UserViewModel GetById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);

            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            var userViewModel = user.MapToViewModel();
            _unitOfWork.Commit();

            return userViewModel;
        }

        public IList<GridOrderViewModel> GetOrdersById(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);

            if(user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }
            var orders = _ordersRepository.GetAll().Where(x => x.User.Id == id);
            var ordersGridModel = orders.Select(x => x.MapToGridViewModel()).ToList();

            _unitOfWork.Commit();
            return ordersGridModel;

        }

        public UserRole GetRole(Guid id)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(id);
            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            return user.Role;
        }

        public void Update(UpdateUserViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetById(model.Id);

            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.Password = model.Password;
            user.Name = model.Name;
            user.Surname = model.Surname;
            user.City = model.City;
            user.Country = model.Country;
            user.PostalCode = model.PostalCode;
            user.Address = model.Address;

            bool emailTaken = _userRepository.emailTaken(user);
            bool userNameTaken = _userRepository.userNameTaken(user);

            if (emailTaken)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.EMAIL_TAKEN);
            }

            if (userNameTaken)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.USERNAME_TAKEN);
            }

            _userRepository.Update(user);
            _unitOfWork.Commit();
        }

        Guid IUserService.RegisterUser(RegisterUserViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var user = new User(model.UserName, model.Password, model.Email, model.Name, model.Surname, model.City, model.Country, model.PostalCode, model.Address);

            if (_userRepository.emailTaken(user))
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.EMAIL_TAKEN);
            }
            var isTaken = _userRepository.userNameTaken(user);
            if (isTaken)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.USERNAME_TAKEN);
            }

            _userRepository.Add(user);
            _unitOfWork.Commit();

            return user.Id;
        }

        Guid IUserService.SignInUser(SignInUserViewModel model)
        {
            _unitOfWork.BeginTransaction();

            var user = _userRepository.GetAll().Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            if (user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }

            var userId = _userRepository.signIn(user);

            _unitOfWork.Commit();
            return userId;
        }
    }
}
