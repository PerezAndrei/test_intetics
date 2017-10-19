using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ims.Domain.IServices;
using ims.Helpers;
using ims.Models;

namespace ims.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IImageService _imageService;

        public HomeController(IUserService userService, IImageService imageService)
        {
            _userService = userService;
            _imageService = imageService;
        }
        [Authorize]
        public ActionResult Index()
        {
            int id = User.Identity.GetUserId<int>();
            var countRecordsOnPage = 11;
            Pagination pagination = new Pagination()
            {
                CountRecords = _imageService.GetImagesCountByUser(id),
                CurrentPageNumber = 1,
                CountRecordsOnPage = countRecordsOnPage
            };
            pagination.SetFirstLastSkipNumbers();
            var images = _imageService.GetImagesByUserForRange(id, pagination.SkipNumber, countRecordsOnPage);
            ViewBag.Images = images;
            ViewBag.Pagination = pagination;
            ViewBag.UserId = id;
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
