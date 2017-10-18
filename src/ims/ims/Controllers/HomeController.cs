using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ims.Domain.IServices;
using ims.Helpers;

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
            int id = User.Identity.GetUserId<int>();
            ViewBag.UserId = id;
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
