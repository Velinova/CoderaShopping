using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using CoderaShopping.Domain;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //#region CRUD

        //[HttpPost]
        //public ActionResult Create(CreateCategoryViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        Helpers.InvalidModelState(ModelState);
        //    }

        //    var categoryId = _categoryService.Create(model);

        //    return Json(categoryId);
        //}
        //[HttpPost]
        //public ActionResult GetProductsById(Guid id)
        //{
        //    var products = _categoryService.GetProductsById(id);

        //    return Json(products);
        //}
        
        [HttpPost]
        public ActionResult GetOrdersCountByMonth(int month)
        {
            var ordersCount = _orderService.GetOrdersCountByMonth(month);
            return Json(ordersCount);
        }


        [HttpPost]
        public ActionResult GetById(Guid id)
        {
            var orderViewModel = _orderService.GetById(id);
            return Json(orderViewModel);
        }

        [HttpPost]
        public ActionResult Update(UpdateOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            _orderService.Update(model);
            return Json(true);
        }
        [HttpPost]
        public ActionResult GetOrderItems(Guid id)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            var orderItems = _orderService.GetOrderItems(id);
            return Json(orderItems);
        }
        [HttpPost]
        public ActionResult GetOrdersAndItemsByUser(Guid userId, SearchParameter parameter)
        {
            var orders = _orderService.GetOrdersAndItemsByUser(userId, parameter);
            return Json(orders);
        }
        //[HttpPost]
        //public JsonResult Delete(Guid id)
        //{
        //    _categoryService.Delete(id);
        //    return Json(true);
        //}

        //#endregion 
        [HttpPost]
        public ActionResult Pay(List<OrderItemAddToOrderViewModel> model, Guid userId)
        {
            _orderService.Pay(model, userId);
            return Json(true);
        }

        [HttpPost]
        public ActionResult GetAll()
        {
            var orders = _orderService.GetAll();
            return Json(orders);
        }

        //[HttpPost]
        //public ActionResult GetActive()
        //{
        //    var categories = _categoryService.GetActive();
        //    return Json(categories);
        //}
    }
}