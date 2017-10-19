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
        int GetImagesCountByUserForTag(int userId, string tagName);
        IEnumerable<ImageVM> GetImagesByUserForRange(int userId, int skipValue, int takeValue);
        IEnumerable<ImageVM> GetImagesByUserForTagForRange(int userId, string tagName, int skipValue, int takeValue);
        void CreateImage();
    }
}