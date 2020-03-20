using CoderaShopping.Domain;
using System;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<OrderItem> GetAllOrderItems(Order order);
        IQueryable<Order> GetOrdersAndItemsByUser(User user, SearchParameter parameter);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

      
        public IQueryable<OrderItem> GetAllOrderItems(Order order)
        {
            return Session.Query<OrderItem>().Where(x => x.Order.Id == order.Id);
        }

        public IQueryable<Order> GetOrdersAndItemsByUser(User user, SearchParameter parameter)
        {
            switch (parameter)
            {
                case SearchParameter.ALL:
                    {
                        return Session.Query<Order>().Where(x => x.User == user);
                    }
                case SearchParameter.NEWEST:
                    {
                        return Session.Query<Order>().Where(x => x.User == user).OrderBy(x=>x.DateOrdered);
                    }
                case SearchParameter.OLDEST:
                    {
                        return Session.Query<Order>().Where(x => x.User == user).OrderByDescending(x => x.DateOrdered);
                    }
                case SearchParameter.DELIEVERED:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.DELIVERED);
                    }
                case SearchParameter.PAID:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.PAID);
                    }
                case SearchParameter.SHIPPED:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.SHIPPED);
                    }
                case SearchParameter.FROZEN:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.FROZEN);
                    }
                case SearchParameter.DISPUTED:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.DISPUTED);
                    }
                case SearchParameter.CANCELED:
                    {
                        return Session.Query<Order>().Where(x => x.User == user && x.Status == OrderStatus.CANCELED);
                    }
                default:
                    return Session.Query<Order>().Where(x => x.User == user);
            }
            
        }
    }
}
