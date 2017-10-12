using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ims.Models
{
    public class ImageVM
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public UserVM User { get; set; }
        public ICollection<TagVM> Tags { get; set; }

        public ImageVM()
        {
            Tags = new List<TagVM>();
        }
    }
}