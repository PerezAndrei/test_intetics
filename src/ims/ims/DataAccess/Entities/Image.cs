using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.DataAccess.Repository;

namespace ims.DataAccess.Entities
{
    public class Image : Entity<int>
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Tag> Tags {get; set; }

        public Image()
        {
            Tags = new List<Tag>();
        }
    }
}