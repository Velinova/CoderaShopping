using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using System;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class OrderItemsController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemsController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public ActionResult Create(OrderItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }

            var itemId = _orderItemService.Create(model);

            return Json(itemId);
        }
        
    }
}