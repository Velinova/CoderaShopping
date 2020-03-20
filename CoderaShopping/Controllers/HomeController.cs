using CoderaShopping.Core;
using System.Web.Mvc;

namespace CoderaShopping.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}