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

        public UserVM GetUserByEmail(string email)
        {
            var user = _repository.GetFirst<User>(filter: u => u.Email.ToLower() == email.ToLower());
            if (user == null) return null;
            var userVM = _mapper.Map<User, UserVM>(user);
            var users = _repository.GetAll<User>();
            var users1 = _mapper.Map<IEnumerable<UserVM>>(users);
            return _mapper.Map<UserVM>(user);
        }

        public void CreateUser(RegisterVM registerVM)
        {
            User user = new User
            {
                Name = registerVM.UserName,
                Email = registerVM.Email,
                Password = registerVM.Password
            };
            _repository.Create<User>(user);
            _repository.Save();
            return;
        }

        public bool EmailExist(string email)
        {
            return _repository.GetExists<User>(u=>u.Email.ToLower() == email.ToLower());
        }
    }
}