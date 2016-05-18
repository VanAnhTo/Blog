using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/
        public ActionResult Index()
        {
            //ViewBag.VanAnh = "Con do";
            var obj = new List<Post>();
            //{
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //    new Person(){FirstName = "Hoang", LastName ="Le", Age = 10},
            //};
            return View(obj);
        }
    }


}