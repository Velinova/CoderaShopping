using CoderaShopping.Business.Mappers;
using CoderaShopping.Business.Models;
using CoderaShopping.DataNHibernate;
using CoderaShopping.DataNHibernate.Repositories;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoderaShopping.Business.Services
{
    public interface IRatingService
    {
        //#region CRUD
        //OrderItemViewModel GetById(Guid id);
        //Guid Create(OrderItemViewModel model);
        //void Update(OrderItemViewModel model);
        //void Delete(Guid id);
        //#endregion

        //IList<RatingViewModel> GetAll();
        double GetAverageRatingValue(Guid ObjectId, RatingObjectType ObjectType);
        IList<RatingViewModel> GetRatingsById(Guid ObjectId, RatingObjectType ObjectType);
        IList<RatingViewModel> GetCommentRatingsById(Guid ObjectId, RatingObjectType ObjectType);
        RatingValuesViewModel GetRatingValues(Guid ObjectId, RatingObjectType ObjectType);
        void SubmitRating(CreateUpdateRatingViewModel model);
        CreateUpdateRatingViewModel CheckRatingUserObject(Guid UserId, Guid ObjectId);
    }

    public class RatingService : IRatingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RatingService(IUserRepository userRepository, IOrderRepository orderRepository, IProductRepository productRepository, IRatingRepository ratingRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _ratingRepository = ratingRepository;
            _unitOfWork = unitOfWork;
        }

        public CreateUpdateRatingViewModel CheckRatingUserObject(Guid userId, Guid objectId)
        {
            _unitOfWork.BeginTransaction();
            var user = _userRepository.GetById(userId);
            if(user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }
            var rating = _ratingRepository.CheckRatingUserObject(user, objectId);
            if(rating == null)
            {
                _unitOfWork.Commit();
                return new CreateUpdateRatingViewModel();
            }
            else
            {
                _unitOfWork.Commit();
                return rating.MapToCreateUpdateViewModel();
            }
            
        }

        public double GetAverageRatingValue(Guid objectId, RatingObjectType objectType)
        {
            _unitOfWork.BeginTransaction();
            if (objectType.Equals(RatingObjectType.ORDER))
            {
                var order = _orderRepository.GetById(objectId);
                if (order == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
                }
                var average = _ratingRepository.GetAverageRatingValue(order);
                _unitOfWork.Commit();
                return average;
            }
            else
            {
                var product = _productRepository.GetById(objectId);
                if (product == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
                }
                var average = _ratingRepository.GetAverageRatingValue(product);
                _unitOfWork.Commit();
                return average;
            }
        }

        public IList<RatingViewModel> GetCommentRatingsById(Guid objectId, RatingObjectType objectType)
        {
            _unitOfWork.BeginTransaction();
            if (objectType.Equals(RatingObjectType.ORDER))
            {
                var order = _orderRepository.GetById(objectId);
                if (order == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
                }
                var ratings = _ratingRepository.GetCommentRatingsById(order);
                var ratingsViewModel = ratings.Select(x => x.MapToViewModel()).ToList();
                return ratingsViewModel;

            }
            else
            {
                var product = _productRepository.GetById(objectId);
                if (product == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
                }
                var ratings = _ratingRepository.GetCommentRatingsById(product);
                var ratingsViewModel = ratings.Select(x => x.MapToViewModel()).ToList();
                return ratingsViewModel;
            }
        }

        public IList<RatingViewModel> GetRatingsById(Guid objectId, RatingObjectType objectType)
        {
            _unitOfWork.BeginTransaction();
            if (objectType.Equals(RatingObjectType.ORDER))
            {
                var order = _orderRepository.GetById(objectId);
                if (order == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
                }
                var ratings = _ratingRepository.GetRatingsById(order);
                var ratingsViewModel = ratings.Select(x => x.MapToViewModel()).ToList();
                return ratingsViewModel;

            }
            else
            {
                var product = _productRepository.GetById(objectId);
                if (product == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
                }
                var ratings = _ratingRepository.GetRatingsById(product);
                var ratingsViewModel = ratings.Select(x => x.MapToViewModel()).ToList();
                return ratingsViewModel;
            }
        }

        public RatingValuesViewModel GetRatingValues(Guid ObjectId, RatingObjectType ObjectType)
        {
            _unitOfWork.BeginTransaction();
            if (ObjectType.Equals(RatingObjectType.ORDER))
            {
                var order = _orderRepository.GetById(ObjectId);
                if (order == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
                }
                var values = new RatingValuesViewModel();
                var ratings = _ratingRepository.GetRatingsById(order);
                values.One = ratings.AsQueryable<Rating>().Where(x => x.Value == 1).ToList().Count;
                values.Two = ratings.AsQueryable<Rating>().Where(x => x.Value == 2).ToList().Count;
                values.Three = ratings.AsQueryable<Rating>().Where(x => x.Value == 3).ToList().Count;
                values.Four = ratings.AsQueryable<Rating>().Where(x => x.Value == 4).ToList().Count;
                values.Five = ratings.AsQueryable<Rating>().Where(x => x.Value == 5).ToList().Count;
                values.Total = values.One + values.Two + values.Three + values.Four + values.Five;
                _unitOfWork.Commit();
                return values;
            }
            else
            {
                var product = _productRepository.GetById(ObjectId);
                if (product == null)
                {
                    _unitOfWork.Commit();
                    throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
                }
                var values = new RatingValuesViewModel();
                var ratings = _ratingRepository.GetRatingsById(product);
                values.One = ratings.AsQueryable<Rating>().Where(x => x.Value == 1).ToList().Count;
                values.Two = ratings.AsQueryable<Rating>().Where(x => x.Value == 2).ToList().Count;
                values.Three = ratings.AsQueryable<Rating>().Where(x => x.Value == 3).ToList().Count;
                values.Four = ratings.AsQueryable<Rating>().Where(x => x.Value == 4).ToList().Count;
                values.Five = ratings.AsQueryable<Rating>().Where(x => x.Value == 5).ToList().Count;
                values.Total = values.One + values.Two + values.Three + values.Four + values.Five;
                _unitOfWork.Commit();
                return values;

            }
        }

        public void SubmitRating(CreateUpdateRatingViewModel model)
        {
            _unitOfWork.BeginTransaction();
            var user = _userRepository.GetById(model.UserId);
            if(user == null)
            {
                _unitOfWork.Commit();
                throw new Exception(ExceptionMessages.UserException.NOT_FOUND);
            }
            var rate = _ratingRepository.GetById(model.Id);
            if (rate == null){ 
                var rating = new Rating(user, model.ObjectId, model.ObjectType, model.Value, model.Comment, model.ShowName);
                _ratingRepository.Add(rating);
                _unitOfWork.Commit();
            }
            else
            {
                rate.Comment = model.Comment;
                rate.Value = model.Value;
                rate.ShowName = model.ShowName;
                _ratingRepository.Update(rate);
                _unitOfWork.Commit();

            }
        }
        
        //public Guid Create(OrderItemViewModel model)
        //{
        //    _unitOfWork.BeginTransaction();
        //    var order = _orderRepository.GetById(model.Order.Id);
        //    if (order == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
        //    }

        //    var product = _productRepository.GetById(model.Product.Id);
        //    if (product == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
        //    }

        //    var orderItem = new OrderItem(order, product, model.Quantity);

        //    _orderItemRepository.Add(orderItem);
        //    _unitOfWork.Commit();
        //    return orderItem.Id;
        //}
        //public void Update(OrderItemViewModel model)
        //{
        //    _unitOfWork.BeginTransaction();
        //    var orderItem = _orderItemRepository.GetById(model.Id);

        //    if (orderItem == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
        //    }

        //    var order = _orderRepository.GetById(model.Order.Id);
        //    if (order == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.OrderException.NOT_FOUND);
        //    }
        //    var product = _productRepository.GetById(model.Product.Id);
        //    if (product == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.ProductException.NOT_FOUND);
        //    }


        //    orderItem.Order = order;
        //    orderItem.Product = product;
        //    orderItem.Quantity = model.Quantity;

        //    _orderItemRepository.Update(orderItem);
        //    _unitOfWork.Commit();

        //}
        //public void Delete(Guid id)
        //{
        //    _unitOfWork.BeginTransaction();
        //    var orderItem = _orderItemRepository.GetById(id);
        //    if (orderItem == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
        //    }
        //    _orderItemRepository.Delete(orderItem);
        //    _unitOfWork.Commit();
        //}
        //public OrderItemViewModel GetById(Guid id)
        //{
        //    _unitOfWork.BeginTransaction();
        //    var orderItem = _orderItemRepository.GetById(id);

        //    if (orderItem == null)
        //    {
        //        _unitOfWork.Commit();
        //        throw new Exception(ExceptionMessages.OrderItemException.NOT_FOUND);
        //    }

        //    var orderItemViewModel = orderItem.MapToViewModel();
        //    _unitOfWork.Commit();

        //    return orderItemViewModel;

        //}

        //public IList<OrderItemViewModel> GetAll()
        //{
        //    _unitOfWork.BeginTransaction();

        //    var orderItem = _orderItemRepository.GetAll();
        //    var orderItemViewModel = orderItem.Select(x => x.MapToViewModel()).ToList();

        //    _unitOfWork.Commit();

        //    return orderItemViewModel;
        //}
    }
}

