
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comments
    {
        public int CommentId { get; set; }
        public int userId { get; set; }
        public int PostId { get; set; }
        public string content { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}