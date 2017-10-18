using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ims.Domain.IServices;

namespace ims.Controllers
{
    [RoutePrefix("api/images")]
    public class ImageController : ApiController
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateImage()
        {
            //if (HttpContext.Current.Request.Files.AllKeys.Any())
            //{
            //    // Get the uploaded image from the Files collection
            //    var httpPostedFile = HttpContext.Current.Request.Files["Image"];
            //    var name = HttpContext.Current.Request.Form["Name"];
            //    var tags = HttpContext.Current.Request.Form["Tags"];
            //    var descr = HttpContext.Current.Request.Form["Description"];
            //    var userId = HttpContext.Current.Request.Form["UserId"];
            //    if (httpPostedFile != null)
            //    {
            //        // Validate the uploaded image(optional)

            //        // Get the complete file path
            //        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), httpPostedFile.FileName);

            //        // Save the uploaded file to "UploadedFiles" folder
            //        httpPostedFile.SaveAs(fileSavePath);
            //    }
            //}
            _imageService.CreateImage();
            return Ok();
        }
        
    }
}
