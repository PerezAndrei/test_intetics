using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class TagVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ImageVM> Images { get; set; }

        public TagVM()
        {
            Images = new List<ImageVM>();
        }
    }
}