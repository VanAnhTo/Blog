using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class UserCreatedPost
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Tittle { get; set; }
        public string Content { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }

        public int CommentId { get; set; }
        //public int userId { get; set; }
        //public int PostId { get; set; }
        public string contentcomment { get; set; }
        public System.DateTime CreatedDateComment { get; set; }
    }
}