using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.Models;

namespace ims.Domain.IServices
{
    public interface ITagService
    {
        IEnumerable<TagVM> GetTags();
        IEnumerable<string> GetNamesOfTags();
        IEnumerable<TagVM> GetTagsPopular();
    }
}