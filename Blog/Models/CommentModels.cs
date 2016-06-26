
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CommentModels
    {
        public int postId { get; set; }

        [DisplayName("Ten")]
        public int userId { get; set; }

        [DisplayName("Noi dung")]
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}