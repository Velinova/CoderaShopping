using CoderaShopping.App_Start;
using CoderaShopping.Business.Models;
using CoderaShopping.Business.Services;
using System;
using System.Net;
using System.Web.Mvc;

namespace CoderaShopping.Controllers.Api
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult GetById(Guid id)
        {
            var user = _userService.GetById(id);
            return Json(user);
        }
        [HttpPost]
        public ActionResult GetOrdersById(Guid id)
        {
            var orders = _userService.GetOrdersById(id);
            return Json(orders);
        }

        [HttpPost]
        public ActionResult GetAllAdmins()
        {
            var admins = _userService.GetAllAdmins();
            return Json(admins);
        }

        [HttpPost]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            return Json(users);
        }

        [HttpPost]
        public ActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Json(users);
        }

        [HttpPost]
        public ActionResult Update(UpdateUserViewModel model)
        {
            _userService.Update(model);
            return Json(true);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            _userService.Delete(id);
            return Json(true);
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }
            var userId = _userService.RegisterUser(model);
            return Json(userId);
        }

        [HttpPost]
        public ActionResult SignInUser(SignInUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Helpers.InvalidModelState(ModelState);
            }
            var userId = _userService.SignInUser(model);
            return Json(userId);
        }
        [HttpPost]
        public ActionResult ChangeRole(Guid id)
        {
            _userService.ChangeRole(id);
            return Json(true);
        }
        [HttpPost]
        public ActionResult GetRole(Guid id)
        {
            var userRole = _userService.GetRole(id);
            return Json(userRole);
        }
        [HttpPost]
        public ActionResult NotAuthorized()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}