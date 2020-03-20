using CoderaShopping.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productsService;

        public ProductsController(IProductService productService)
        {
            _productsService = productService;
        }

        #region CRUD

        [HttpPost]
        public ActionResult Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }
            var productId = _productsService.Create(model);
            return Json(productId);
        }

        [HttpPost]
        public ActionResult Update(UpdateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }
            _productsService.Update(model);
            return Json(true);
        }

        [HttpPost]
        public ActionResult GetById(Guid id)
        {
            var productViewModel = _productsService.GetById(id);
            return Json(productViewModel);
        }
        #endregion

        [HttpPost]
        public ActionResult GetAll()
        {
            var products = _productsService.GetAll();
            return Json(products);
        }

        [HttpPost]
        public ActionResult GetProductsInStock()
        {
            var productsInStock = _productsService.GetProductsInStock();
            return Json(productsInStock);
        }
        [HttpPost]
        public ActionResult Delete(Guid id)
        {
             _productsService.Delete(id);
            return Json(true);
        }
    }
}