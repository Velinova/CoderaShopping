using CoderaShopping.Domain;
using System;
using System.Linq;

namespace CoderaShopping.DataNHibernate.Repositories
{
    public interface IRatingRepository : IRepository<Rating>
    {
        double GetAverageRatingValue(Order order);
        double GetAverageRatingValue(Product product);
        IQueryable<Rating> GetRatingsById(Order order);
        IQueryable<Rating> GetRatingsById(Product product);
        IQueryable<Rating> GetCommentRatingsById(Order order);
        IQueryable<Rating> GetCommentRatingsById(Product product);
        Rating CheckRatingUserObject(User user, Guid ObjectId);
    }

    public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
    {
        public RatingRepository(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public Rating CheckRatingUserObject(User user, Guid objectId)
        {
          return Session.Query<Rating>().Where(x => x.ObjectId == objectId && x.User == user).SingleOrDefault();
        }

        public double GetAverageRatingValue(Order order)
        {
            var ratings = GetRatingsById(order);
            var sum = 0;
            foreach (var rating in ratings)
            {
                sum += rating.Value;
            }
            var average = sum / ratings.ToList().Count;
            return average;
        }

        public double GetAverageRatingValue(Product product)
        {
            var ratings = GetRatingsById(product);
            var sum = 0;
            foreach (var rating in ratings)
            {
                sum += rating.Value;
            }
            if(ratings.ToList().Count == 0)
            {
                return 0;
            }
            var average = sum / ratings.ToList().Count;
            return average;
        }

        public IQueryable<Rating> GetCommentRatingsById(Order order)
        {
            return Session.Query<Rating>().Where(x => x.ObjectId == order.Id && x.Comment != null);
        }

        public IQueryable<Rating> GetCommentRatingsById(Product product)
        {
            return Session.Query<Rating>().Where(x => x.ObjectId == product.Id && x.Comment != null);
        }

        public IQueryable<Rating> GetRatingsById(Order order)
        {
            return Session.Query<Rating>().Where(x => x.ObjectId == order.Id);
        }

        public IQueryable<Rating> GetRatingsById(Product product)
        {
            return Session.Query<Rating>().Where(x => x.ObjectId == product.Id);
        }
    }
}
