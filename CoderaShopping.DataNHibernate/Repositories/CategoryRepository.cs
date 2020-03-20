using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        bool CheckUniqueness(Category category);
        Category GetDefaultCategory(Guid? categoryId);
        IQueryable<Category> GetActive();
    }

    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool CheckUniqueness(Category category)
        {
            bool hasOthersWithSameName;

            // if it is create
            if (category.Id == Guid.Empty)
            {
                hasOthersWithSameName = Session.Query<Category>()
                    .Any(x => x.Name == category.Name);

            }
            // it is update
            else
            {
                hasOthersWithSameName = Session.Query<Category>()
                    .Any(x => x.Id != category.Id && x.Name == category.Name);
            }

            return !hasOthersWithSameName;
        }



        public Category GetDefaultCategory(Guid? categoryId)
        {
            // update
            if (categoryId.HasValue)
            {
                return Session.Query<Category>().FirstOrDefault(x => x.Id != categoryId && x.IsDefault);
            }

            return Session.Query<Category>().FirstOrDefault(x => x.IsDefault);
        }


        public IQueryable<Category> GetActive()
        {
            return Session.Query<Category>().Where(x => x.Status);
        }
    }
}
