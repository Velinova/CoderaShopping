using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public class Product
    {
        private readonly Guid _id = default;
        private string _name;
        private string _description;
        private int _quantity;
        private double _price;
        private Category _category;
        private Manifacturer _manifacturer;
        private IList<OrderItem> _orderItems;
        //private IList<Rating> _ratings;

        #region Constructors
        public Product() {
            _orderItems = new List<OrderItem>();
        }
        public Product (string name, string description, int quantity, double price, Category category, Manifacturer manifacturer) {
            _name = name;
            _description = description;
            _quantity = quantity;
            _category = category;
            _manifacturer = manifacturer;
            _price = price;
            _orderItems = new List<OrderItem>();
            //_ratings = new List<Rating>();
        }
        #endregion

        #region Properties
        public virtual Guid Id
        {
            get { return _id; }
        }
        public virtual string Name {
            get { return _name; }
            set { _name = value; }
        }
        public virtual string Description {
            get { return _description; }
            set { _description = value; }
        }
        public virtual int Quantity {
            get { return _quantity; }
            set { _quantity = value; }
        }
        public virtual Category Category {
            get { return _category; }
            set { _category = value; }
        }
        public virtual Manifacturer Manifacturer
        {
            get { return _manifacturer; }
            set { _manifacturer = value; }
        }
        public virtual double Price
        {
            get { return _price; }
            set { _price = value; }
        }
        public virtual IList<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }
        //public virtual IList<Rating> Ratings
        //{
        //    get { return _ratings; }
        //    set { _ratings = value; }
        //}
        #endregion

    }
}
