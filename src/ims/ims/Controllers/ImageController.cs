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
            _imageService.CreateImage();
            return Ok();
        }
        
    }
}
