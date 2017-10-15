using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ims.Domain.IServices;
using ims.Models;

namespace ims.Controllers
{
    public class AccountController : Controller
    {

        public readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInVM model)
        {
            return View();
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(RegisterVM model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userExist = _userService.EmailExist(model.Email);

            if (userExist)
            {
                ModelState.AddModelError("Email", "User with this email already exists");
                return View(model);
            }

            _userService.CreateUser(model);
            return View();
        }
    }
}