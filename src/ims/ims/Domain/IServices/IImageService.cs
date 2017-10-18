using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.Models;

namespace ims.Domain.IServices
{
    public interface IImageService
    {
        IEnumerable<ImageVM> GetImagesByUser(int userId);
        void CreateImage();
    }
}