using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Blog.Models
{
    public class PostInsertModel
    {

        public int PostId { get; set; }

        [DisplayName("Tieu de 1111")]
        public string Tittle { get; set; }

        [DisplayName("Noi dung 1111")]
        public string Content { get; set; }

        public int CreatorId { get; set; }

        public int CategoryId { get; set; }

        [DisplayName("Nhom bai")]
        public int SelectedCat { get; set; }

        public SelectList CategoryItems { get; set; }

    }

}