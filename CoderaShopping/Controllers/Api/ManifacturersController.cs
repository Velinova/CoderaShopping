using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using System;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class ManifacturersController : Controller
    {
        private readonly IManifacturerService _manifacturerService;

        public ManifacturersController(IManifacturerService manifacturerService)
        {
            _manifacturerService = manifacturerService;
        }

        //#region CRUD

        [HttpPost]
        public ActionResult Create(CreateManifacturerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            var manifacturer = _manifacturerService.Create(model);

            return Json(manifacturer);
        }
        //[HttpPost]
        //public ActionResult GetProductsById(Guid id)
        //{
        //    var products = _categoryService.GetProductsById(id);

        //    return Json(products);
        //}

        [HttpPost]
        public ActionResult GetById(Guid id)

        {
            var manifacturerViewModel = _manifacturerService.GetById(id);
            return Json(manifacturerViewModel);
        }

        [HttpPost]
        public ActionResult Update(ManifacturerViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            _manifacturerService.Update(model);
            return Json(true);
        }

        [HttpPost]
        public JsonResult Delete(Guid id)
        {
            _manifacturerService.Delete(id);
            return Json(true);
        }

        

        [HttpPost]
        public ActionResult GetAll()
        {
            var manifacturers = _manifacturerService.GetAll();
            return Json(manifacturers);
        }

        //[HttpPost]
        //public ActionResult GetActive()
        //{
        //    var categories = _categoryService.GetActive();
        //    return Json(categories);
        //}
    }
}