using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ims.Domain.IServices;

namespace ims.Controllers
{
    public class HomeController : Controller
    {
        public readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            //var users = _userService.GetUsers();
            return View();
        }
    }
}
