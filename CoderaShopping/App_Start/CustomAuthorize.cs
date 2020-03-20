using CoderaShopping.Core;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CoderaShopping.App_Start
{
    public class CustomAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userId = filterContext.RequestContext.HttpContext?.Request?.Cookies["userId"]?.Value.ToString();
            var userRole = filterContext.RequestContext.HttpContext?.Request?.Cookies["userRole"]?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Users" },
                                              { "Action", "NotAuthorized" } });
                return;
            }

            Context.UserId = new Guid(userId);
            Context.UserRole = int.Parse(userRole);
        }
    }
}