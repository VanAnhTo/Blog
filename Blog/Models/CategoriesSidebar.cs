using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class CategoriesSidebar
    {
        public int CategoryId { get; set; }

        public int PostId { get; set; }
        public string CategoryName { get; set; }

        public string CreatorName { get; set; }
        public DateTime CreatedDate { get; set; }

        public string Tittle { get; set; }
        public string Content { get; set; }
    }
}