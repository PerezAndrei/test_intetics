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
    public class TagService : ITagService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public TagService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<TagVM> GetTags()
        {
            var users = _repository.GetAll<Tag>();
            return _mapper.Map<IEnumerable<TagVM>>(users);
        }

        public IEnumerable<string> GetNamesOfTags()
        {
            var names = _repository.GetAll<Tag>().Select(t => t.Name);
            return names;
        }
    }
}