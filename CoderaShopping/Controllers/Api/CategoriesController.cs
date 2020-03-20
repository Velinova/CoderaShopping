using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using System;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region CRUD

        [HttpPost]
        public ActionResult Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            var categoryId = _categoryService.Create(model);

            return Json(categoryId);
        }
        [HttpPost]
        public ActionResult GetProductsById(Guid id)
        {
            var products = _categoryService.GetProductsById(id);

            return Json(products);
        }

        [HttpPost]
        public ActionResult GetById(Guid id)
        {
            var categoryViewModel = _categoryService.GetById(id);
            return Json(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            _categoryService.Update(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            _categoryService.Delete(id);
            return Json(true);
        }

        #endregion

        [HttpPost]
        public ActionResult GetAll()
        {
            var categories = _categoryService.GetAll();
            return Json(categories);
        }

        [HttpPost]
        public ActionResult GetActive()
        {
            var categories = _categoryService.GetActive();
            return Json(categories);
        }
        [HttpPost]
        public ActionResult GetDefault()
        {
            var category = _categoryService.GetDefault();
            return Json(category);
        }
    }
}