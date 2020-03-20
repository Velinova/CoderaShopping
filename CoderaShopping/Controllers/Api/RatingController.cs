using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using CoderaShopping.Domain;
using System;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class RatingController : Controller
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService;
        }

        [HttpPost]
        public ActionResult GetAverageRatingValue(Guid id, RatingObjectType type) //0 order 1 product
        {
           
            var average = _ratingService.GetAverageRatingValue(id, type);

            return Json(average);
        }
        [HttpPost]
        public ActionResult GetCommentRatingsById(Guid id, RatingObjectType type) //0 order 1 product
        {

            var ratings = _ratingService.GetCommentRatingsById(id, type);

            return Json(ratings);
        }
        [HttpPost]
        public ActionResult GetRatingValues(Guid id, RatingObjectType type) //0 order 1 product
        {

            var values = _ratingService.GetRatingValues(id, type);

            return Json(values);
        }
        [HttpPost]
        public ActionResult SubmitRating(CreateUpdateRatingViewModel model) 
        {

            _ratingService.SubmitRating(model);

            return Json(true) ;
        }
        
        [HttpPost]
        public ActionResult CheckRatingUserObject(Guid userId, Guid objectId)
        {

            var model = _ratingService.CheckRatingUserObject(userId, objectId);

            return Json(model);
        }
    }
}