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
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;
        public UserService(IMapper mapper, IRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public IEnumerable<UserVM> GetUsers()
        {
            var users = _repository.GetAll<User>();
            return _mapper.Map<IEnumerable<UserVM>>(users);
        }

        public void CreateUser(UserVM userVM)
        {
            return;
        }
    }
}