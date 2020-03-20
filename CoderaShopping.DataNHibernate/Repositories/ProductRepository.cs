using CoderaShopping.Domain;
using System;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        bool CheckUniqueness(Product product);
        IQueryable<Product> GetProductsInStock();
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool CheckUniqueness(Product product)
        {
            bool hasOthersWithSameName;

            // create
            if (product.Id == Guid.Empty)
            {
                hasOthersWithSameName = Session.Query<Product>()
                    .Any(x => x.Name == product.Name);

            }
            // update
            else
            {
                hasOthersWithSameName = Session.Query<Product>()
                    .Any(x => x.Id != product.Id && x.Name == product.Name);
            }

            return !hasOthersWithSameName;
        }

        public IQueryable<Product> GetProductsInStock()
        {
            return Session.Query<Product>().Where(x => x.Quantity > 0);
        }
    }
}
