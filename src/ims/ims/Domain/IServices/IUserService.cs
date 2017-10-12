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
        void CreateUser(UserVM userVM);
    }
}