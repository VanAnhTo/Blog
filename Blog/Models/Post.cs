using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public int CreatorId { get; set; }

        public string CreatorName { get; set; }
        
        public DateTime CreatedDate { get; set; }

    }
}