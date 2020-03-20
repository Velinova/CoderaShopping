using CoderaShopping.Domain;
using System;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IManifacturerRepository : IRepository<Manifacturer>
    {
        bool CheckUniqueness(Manifacturer manifacturer);
    }

    public class ManifacturerRepository : RepositoryBase<Manifacturer>, IManifacturerRepository
    {
        public ManifacturerRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public bool CheckUniqueness(Manifacturer manifacturer)
        {
            bool hasOthersWithSameName;

            // if it is create
            if (manifacturer.Id == Guid.Empty)
            {
                hasOthersWithSameName = Session.Query<Manifacturer>()
                    .Any(x => x.Name == manifacturer.Name);

            }
            // it is update
            else
            {
                hasOthersWithSameName = Session.Query<Manifacturer>()
                    .Any(x => x.Id != manifacturer.Id && x.Name == manifacturer.Name);
            }

            return !hasOthersWithSameName;
        }

    }
}
