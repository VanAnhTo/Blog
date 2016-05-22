using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class LatestPosts
    {
        public int PostId { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Tittle { get; set; }
    }
}