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
        //public string CategoryName { get; set; }

        // public List<Category> categoryList;

       // public List<Category> cate;

        [DisplayName("Nhom bai")]
        public int SelectedCat { get; set; }
        //public List<Category> cate;
        //public SelectList CategoryList { get; set; }  
        //public IEnumerable<SelectListItem> CategoryItems
        //{
        //    get { return new SelectList(cate, "CategoryId", "CategoryName"); }
        //    set {  }
        //}
       
    }

}