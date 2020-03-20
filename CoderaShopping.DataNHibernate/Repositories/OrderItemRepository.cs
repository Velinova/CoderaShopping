using CoderaShopping.Domain;
using System;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        
    }

    public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

      
       
    }
}
