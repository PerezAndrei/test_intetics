using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ims.Domain.IServices;
using ims.Models;

namespace ims.Controllers
{
    [RoutePrefix("api/tags")]
    public class TagController : ApiController
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetTags()
        {
            return Ok(_tagService.GetTags());
        }

        [HttpGet]
        [Route("names")]
        public IHttpActionResult GetNamesOfTags()
        {
            return Ok(_tagService.GetNamesOfTags());
        }

        [HttpGet]
        [Route("popular")]
        public IHttpActionResult GetTagsPopular()
        {
            var tags = _tagService.GetTagsPopular();
            return Ok(tags ?? new TagVM [0]);
        }


    }
}
