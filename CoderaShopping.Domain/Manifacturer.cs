using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public class Manifacturer
    {
        private readonly Guid _id = default;
        private string _name;
        private string _city;
        private string _country;
        private string _address;
        private IList<Product> _products;

        #region Constructors
        public Manifacturer()
        {
            _products = new List<Product>();
        }
        public Manifacturer(string name, string city, string country, string address)
        {
            _products = new List<Product>();
            _name = name;
            _city = city;
            _country = country;
            _address = address;
        }
        #endregion

        #region Properties
        public virtual Guid Id
        {
            get { return _id; }
        }
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
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
        public virtual string Address
        {
            get { return _address; }
            set { _address = value; }

        }
        public virtual IList<Product> Products
        {
            get { return _products; }
            set { _products = value; }
        }
        #endregion
    }
}
