using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ims.Domain.IServices;
using ims.Models;

namespace ims.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IImageService _imageService;

        public UserController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        [Route("{userId:int}/images/page/{pageNumber:int}")]
        public IHttpActionResult GetImagesByUserId(int userId, int pageNumber)
        {
            var countRecordsOnPage = 11;
            Pagination pagination = new Pagination()
            {
                CountRecords = _imageService.GetImagesCountByUser(userId),
                CurrentPageNumber = pageNumber,
                CountRecordsOnPage = countRecordsOnPage
            };
            pagination.SetFirstLastSkipNumbers();
            var images = _imageService.GetImagesByUserForRange(userId, pagination.SkipNumber, countRecordsOnPage);
            if (images == null)
            {
                images = new ImageVM[0];
            }
            return Ok(new {images, pagination});
        }

        [HttpGet]
        [Route("{userId:int}/images/page/{pageNumber:int}/tag/{tagName}")]
        public IHttpActionResult GetImagesByUserIdForTag(int userId, int pageNumber, string tagName)
        {
            var countRecordsOnPage = 11;
            Pagination pagination = new Pagination()
            {
                CountRecords = _imageService.GetImagesCountByUserForTag(userId, tagName),
                CurrentPageNumber = pageNumber,
                CountRecordsOnPage = countRecordsOnPage
            };
            pagination.SetFirstLastSkipNumbers();
            var images = _imageService.GetImagesByUserForTagForRange(userId, tagName, pagination.SkipNumber, countRecordsOnPage);
            if (images == null)
            {
                images = new ImageVM[0];
            }
            return Ok(new { images, pagination });
        }

    }
}
