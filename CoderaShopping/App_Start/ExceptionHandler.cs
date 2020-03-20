using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace CoderaShopping.App_Start
{
    public class ExceptionHandler : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            filterContext.HttpContext.Response.ContentType = "application/json";
            filterContext.HttpContext.Response.ClearContent();
            filterContext.HttpContext.Response.Write(JsonConvert.SerializeObject(filterContext.Exception.Message));

            filterContext.ExceptionHandled = true;
        }
    }
}