
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class Comments
    {
        public int userId { get; set; }
        public string content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}