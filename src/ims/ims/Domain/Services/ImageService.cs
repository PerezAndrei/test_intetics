using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using ims.Domain.IServices;
using ims.Models;
using ims.DataAccess.Entities;
using AutoMapper;
using ims.DataAccess.Repository;
//using Ninject.Activation;

namespace ims.Domain.Services
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public ImageService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public IEnumerable<ImageVM> GetImagesByUser(int userId)
        {
            var images = _repository.Get<Image>(filter: i => i.UserId == userId);
            return _mapper.Map<IEnumerable<ImageVM>>(images);
        }

        public void CreateImage()
        {
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                Image newImage = BuildImage();
                if (newImage == null)
                {
                    PostError();
                }
                _repository.Create<Image>(newImage);
                _repository.Save();
                var httpPostedFile = HttpContext.Current.Request.Files["Image"];
                if (httpPostedFile != null)
                {
                    var catalogPath = Path.Combine(HttpContext.Current.Server.MapPath($"~/Images"), newImage.UserId.ToString()); 
                    CheckCatalogExist(catalogPath);
                    var imageName = $"{newImage.Id}_{httpPostedFile.FileName}";
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath($"~/Images/{newImage.UserId}/"),
                        imageName);
                    httpPostedFile.SaveAs(fileSavePath);
                    var filePathForBrowser = $"../../Images/{newImage.UserId}/{imageName}";
                    newImage.Path = filePathForBrowser;
                    _repository.Save();
                }
                else
                {
                    PostError();
                }
            }
            else
            {
                PostError();
            }
        }

        public int GetImagesCountByUser(int userId)
        {
            var count = _repository.GetCount<Image>(filter: i => i.UserId == userId);
            return count;
        }

        public IEnumerable<ImageVM> GetImagesByUserForRange(int userId, int skipValue, int takeValue)
        {
            var images = _repository.Get<Image>(filter: i => i.UserId == userId,  orderByDescending: o => o.OrderByDescending(i => i.Id), skip: skipValue, take: takeValue);
            return _mapper.Map<IEnumerable<ImageVM>>(images);
        }

        public IEnumerable<ImageVM> GetImagesByUserForTagForRange(int userId, string tagName, int skipValue, int takeValue)
        {
            var images = _repository.Get<Image>(filter: i => i.UserId == userId && i.Tags.Any(t => t.Name == tagName), orderByDescending: o => o.OrderByDescending(i => i.Id), skip: skipValue, take: takeValue);
            return _mapper.Map<IEnumerable<ImageVM>>(images);
        }

        public int GetImagesCountByUserForTag(int userId, string tagName)
        {
            var count =
                _repository.GetCount<Image>(filter: i => i.UserId == userId && i.Tags.Any(t => t.Name == tagName));
            return count;
        }

        #region Private methods

        private void PostError()
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            throw new HttpResponseException(httpResponseMessage);
        }
        private Image BuildImage()
        {
            var name = HttpContext.Current.Request.Form["Name"];
            var tags = HttpContext.Current.Request.Form["Tags"];
            var descr = HttpContext.Current.Request.Form["Description"];
            var userIdStr = HttpContext.Current.Request.Form["UserId"];
            Image image = new Image();

            if (!string.IsNullOrWhiteSpace(name))
            {
                image.Name = name;
            }

            if (!string.IsNullOrWhiteSpace(tags))
            {
                var tagsArray = tags.Trim().Split(',');
                if (tagsArray.Length > 0)
                {
                    image.Tags = GetTagsByNames(tagsArray);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

            if (!string.IsNullOrWhiteSpace(descr))
            {
                image.Description = descr;
            }

            if (!string.IsNullOrWhiteSpace(userIdStr))
            {
                int userId;
                if (int.TryParse(userIdStr, out userId))
                {
                    image.UserId = userId;
                }
                else
                {
                    return null;
                }

            }
            return image;
        }

        private ICollection<Tag> GetTagsByNames(string[] names)
        {
            ICollection<Tag> tags = new List<Tag>();
            foreach (var name in names)
            {
                var tagExist = _repository.GetFirst<Tag>(filter: t => t.Name == name.ToLower());
                if (tagExist == null)
                {
                    Tag tag = new Tag
                    {
                        Id = 0,
                        Name = name.ToLower()
                    };
                    //_repository.Create<Tag>(tag);
                    //_repository.Save();
                    tags.Add(tag);
                }
                else
                {
                    tags.Add(tagExist);
                }
            }
            return tags;
        }

        private void CheckCatalogExist(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion
    }
}