using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.Models;

namespace ims.Domain.IServices
{
    public interface IUserService
    {
        IEnumerable<UserVM> GetUsers();
        UserVM GetUserByEmail(string email);
        void CreateUser(RegisterVM registerVM);
        bool EmailExist(string email);
    }
}