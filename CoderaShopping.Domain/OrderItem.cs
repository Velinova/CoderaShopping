using System;
using CoderaShopping.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoderaShopping.Domain
{
    public class OrderItem
    {
        private readonly Guid _id = default;
        private Order _order;
        private Product _product;
        private int _quantity;

        #region Constructors

        public OrderItem()
        {

        }
        public OrderItem(Order order, Product product, int quantity)
        {
            _order = order;
            _product = product;
            _quantity = quantity;
        }
        #endregion

        #region Properties
        public virtual Guid Id
        {
            get { return _id; }
        }
        public virtual Order Order
        {
            get { return _order; }
            set { _order = value; }
        }
        public virtual Product Product
        {
            get { return _product; }
            set { _product = value; }
        }
        public virtual int Quantity
        {
            get { return _quantity;}
            set { _quantity = value; }
        }
        #endregion
    }
}
