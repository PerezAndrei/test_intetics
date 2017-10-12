using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.DataAccess.Repository;

namespace ims.DataAccess.Entities
{
    public class User : Entity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Image> Images { get; set; }

        public User()
        {
            Images = new List<Image>();
        }
    }
}