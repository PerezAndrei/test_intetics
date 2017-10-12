using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.Domain.IServices;
using ims.Models;
using ims.DataAccess.Entities;
using AutoMapper;
using ims.DataAccess.Repository;

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

        public void CreateImage(ImageVM imageVM)
        {
            return;
        }
    }
}