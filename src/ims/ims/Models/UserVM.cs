using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<ImageVM> Images { get; set; }

        public UserVM()
        {
            Images = new List<ImageVM>();
        }
    }
}