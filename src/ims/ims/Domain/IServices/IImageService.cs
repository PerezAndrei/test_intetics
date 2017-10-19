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
        int GetImagesCountByUser(int userId);
        IEnumerable<ImageVM> GetImagesByUserForRange(int userId, int skipValue, int takeValue);
        void CreateImage();
    }
}