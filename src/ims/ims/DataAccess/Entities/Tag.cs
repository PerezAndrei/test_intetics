using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ims.DataAccess.Repository;

namespace ims.DataAccess.Entities
{
    public class Tag : Entity<int>
    {
        public string Name { get; set; }
        public ICollection<Image> Images { get; set; }

        public Tag()
        {
            Images = new List<Image>();
        }
    }
}