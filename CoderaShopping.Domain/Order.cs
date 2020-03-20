using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public enum SearchParameter
    {
        ALL,
        NEWEST,
        OLDEST,
        DELIEVERED,
        PAID,
        SHIPPED,
        FROZEN,
        DISPUTED,
        CANCELED
    }
    public enum OrderStatus
    {
        PAID,
        SHIPPED,
        DELIVERED,
        FROZEN,
        CANCELED,
        DISPUTED
    }
   
    public class Order
    {
        private readonly Guid _id = default;
        private User _user;
        //private IList<Rating> _ratings;
        private OrderStatus _status;
        private DateTime _dateOrdered;
        private DateTime _dateDelivered;
        private int _orderPrice;
        private IList<OrderItem> _orderItems;

        public static OrderStatus MapToOrderStatus(int n)
        {
            switch (n)
            {
                case 0: return OrderStatus.PAID;
                case 1: return OrderStatus.SHIPPED;
                case 2: return OrderStatus.DELIVERED;
                case 3: return OrderStatus.FROZEN;
                case 4: return OrderStatus.CANCELED;
                case 5: return OrderStatus.DISPUTED;
            }
            // unreachable code
            return OrderStatus.PAID;
        }

        #region Constructors
        public Order()
        {
            _orderItems = new List<OrderItem>();
            //_ratings = new List<Rating>();
        }

        public Order(User user, OrderStatus status, DateTime dateOrdered, int orderPrice)
        {
            _user = user;
            _status = status;
            _dateOrdered = dateOrdered;
            _orderPrice = orderPrice;
            _orderItems = new List<OrderItem>();
            //_ratings = new List<Rating>();

        }
        #endregion

        #region Properties
        public virtual Guid Id
        {
            get { return _id; }
        }
        public virtual User User
        {
            get { return _user; }
            set { _user = value; }
        }
        //public virtual IList<Rating> Ratings
        //{
        //    get { return _ratings; }
        //    set { _ratings = value; }
        //}
        public virtual OrderStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }
        public virtual DateTime DateOrdered
        {
            get { return _dateOrdered; }
            set { _dateOrdered = value; }
        }
        public virtual DateTime DateDelivered
        {
            get { return _dateDelivered; }
            set { _dateDelivered = value; }
        }
        public virtual int OrderPrice
        {
            get { return _orderPrice; }
            set { _orderPrice = value; }
        }
        public virtual IList<OrderItem> OrderItems
        {
            get { return _orderItems; }
            set { _orderItems = value; }
        }
        #endregion
    }
}
