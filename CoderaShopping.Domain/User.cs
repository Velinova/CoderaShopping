using System;
using System.Collections.Generic;

namespace CoderaShopping.Domain
{
    public enum UserRole
    {
        EXTERNAL,
        INTERNAL
    };
    public class User
    {
        private readonly Guid _id = default;
        private UserRole _role;
        private string _userName;
        private string _email;
        private string _password;
        private string _name;
        private string _surname;
        private string _city;
        private string _country;
        private string _postalCode;
        private string _address;
        private IList<Order> _orders;
        private IList<Rating> _ratings;
        public virtual Guid Id
        {
            get { return _id; }
        }
        public virtual UserRole Role
        {
            get { return _role; }
            set { _role = value; }
        }

        public virtual string UserName
        {
            get {return _userName; }
            set {_userName = value; }
        }
        public virtual string Email
        {
            get { return _email; }
            set { _email = value; }
        }
        public virtual string Password
        {
            get { return _password; }
            set { _password = value; }
        }
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public virtual string Surname
        {
            get { return _surname; }
            set { _surname = value; }

        }
        public virtual string City
        {
            get { return _city; }
            set { _city = value; }
        }
        public virtual string Country
        {
            get { return _country; }
            set { _country = value; }
        }
        public virtual string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }
        public virtual string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        public virtual IList<Order> Orders
        {
            get { return _orders; }
            set { _orders = value; }
        }
        public virtual IList<Rating> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }
        #region Constructors
        public User()
        {
            _orders = new List<Order>();
            _ratings = new List<Rating>();
        }
        public User(string UserName, string Password, string Email, string Name, string Surname, string City, string Country, string PostalCode, string Adress) {
            _userName = UserName;
            _email = Email;
            _password = Password;
            _name = Name;
            _surname = Surname;
            _city = City;
            _country = Country;
            _address = Adress;
            _postalCode = PostalCode;
            _role = UserRole.EXTERNAL;
            _orders = new List<Order>();
            _ratings = new List<Rating>();
        }
        #endregion
    }

}
