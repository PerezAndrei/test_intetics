using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using ims.Domain.IServices;
using ims.Models;
using Microsoft.Owin.Security;

namespace ims.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

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
            if (ModelState.IsValid)
            {
                UserVM user = _userService.GetUserByEmail(model.Email);

                if (user == null || user.Password != model.Password)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                    return View(model);
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult LogOut()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
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
            LogInVM logIn = new LogInVM()
            {
                Email = model.Email,
                Password = model.Password
            };
            return RedirectToAction("LogIn", "Account", logIn);
        }
    }
}