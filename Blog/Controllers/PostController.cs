using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web.Security;



namespace Blog.Controllers
{
    public class PostController : Controller
    {

        private BlogEntities blog = new BlogEntities();

        private IList<Post> allPost = new List<Post>();

        // GET: /Post/
        public ActionResult Index(int page = 0, string search = null)
        {

            const int pageSize = 3; // you can always do something more elegant to set this
            var count = blog.Posts.Count();



            var list = (from d in blog.Posts
                        join b in blog.Users on d.CreatorId equals b.UserId
                        select new UserCreatedPost()
                        {
                            Tittle = d.Tittle,
                            CreatedDate = d.CreatedDate,
                            PostId = d.PostId,
                            CategoryId = d.CategoryId,
                            UserName = b.UserName,
                            Content = d.Content
                        }).OrderByDescending(e => e.CreatedDate).Skip(page * pageSize).Take(pageSize).ToList();

            IEnumerable<Post> post = blog.Posts.Where(
                p => search == null
                || p.Content.Contains(search)
                || p.Tittle.Contains(search));

            //ViewBag.CurrentPage = page;           
            //ViewBag.TotalPages = Math.Ceiling((double)blog.Posts.Count() / pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Search = search;

            return View(list);
        }

        [HttpPost]
        public ActionResult Search(string search = null)
        {
            const int page = 0;
            const int pageSize = 3; // you can always do something more elegant to set this
            //var count = blog.Posts.Count();



            //var list = (from d in blog.Posts
            //            join b in blog.Users on d.CreatorId equals b.UserId
            //            select new UserCreatedPost()
            //            {
            //                Tittle = d.Tittle,
            //                CreatedDate = d.CreatedDate,
            //                PostId = d.PostId,
            //                CategoryId = d.CategoryId,
            //                UserName = b.UserName,
            //                Content = d.Content
            //            }).OrderByDescending(e => e.CreatedDate).Skip(page * pageSize).Take(pageSize).ToList();

            var count = blog.Posts.Where(
                p => search == null
                || p.Content.Contains(search)
                || p.Tittle.Contains(search)).Count();

            var list = (from d in blog.Posts
                        join b in blog.Users on d.CreatorId equals b.UserId
                        where d.Tittle.Contains(search) || d.Content.Contains(search)
                        select new UserCreatedPost()
                        {
                            Tittle = d.Tittle,
                            CreatedDate = d.CreatedDate,
                            PostId = d.PostId,
                            CategoryId = d.CategoryId,
                            UserName = b.UserName,
                            Content = d.Content
                        }).OrderByDescending(e => e.CreatedDate).Skip(page * pageSize).Take(pageSize).ToList();

            //ViewBag.CurrentPage = page;           
            //ViewBag.TotalPages = Math.Ceiling((double)blog.Posts.Count() / pageSize);
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Search = search;

            return View("Index", list);
        }

        //public ActionResult Search(string search = null)
        //{
        //    IEnumerable<Post> post = blog.Posts.Where(
        //       p => search == null
        //       || p.Content.Contains(search)
        //       || p.Tittle.Contains(search));

        //    return (ViewBag.Search = search);
        //}

        public ActionResult PostsByCategory(int categoryId, int take)
        {
            var list = blog.Posts.Where(e => e.CategoryId == categoryId).Take(take).ToList();
            return View("Category", list);
        }

        public PartialViewResult Sidebars()
        {
            //var list = blog.Posts.Take(10).ToList();           
            var list = (from d in blog.Posts
                        select new LatestPosts()
                        {
                            Tittle = d.Tittle,
                            CreatedDate = d.CreatedDate,
                            PostId = d.PostId
                        }).Take(10);
            return PartialView("Sidebars", list);

        }


        public ActionResult PostsInCategory(int categoryId)
        {

            var list = (from d in blog.Posts
                        join b in blog.Categories on d.CategoryId equals b.CategoryId
                        join u in blog.Users on d.CreatorId equals u.UserId
                        where d.CategoryId == categoryId
                        select new CategoriesSidebar()
                        {
                            Content = d.Content,
                            CreatedDate = d.CreatedDate,
                            CreatorName = u.UserName,
                            CategoryId = d.CategoryId,
                            Tittle = d.Tittle,
                            PostId = d.PostId,
                            CategoryName = b.Name
                        });
            return View("PostsInCategory", list);
        }

        public PartialViewResult DoCategoriesSidebars()
        {
            //var list = blog.Posts.Take(10).ToList();


            var list = (from d in blog.Posts
                        join b in blog.Categories on d.CategoryId equals b.CategoryId
                        select new CategoriesSidebar()
                        {
                            CategoryId = d.CategoryId,
                            CategoryName = b.Name
                        }).Take(10);
            return PartialView("CategoriesSidebars", list);

        }

        public PartialViewResult ViewComment(int id)
        {
            var list = (from d in blog.Comments
                        where d.PostId == id
                        select new Comments()
                        {
                            content = d.Content,
                            CreatedDate = d.CreatedDate,
                            userId = d.UserId
                        }).Take(10);
            return PartialView("_Comments", list);

        }

        // GET: /Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var post = (from d in blog.Posts
                        join b in blog.Users on d.CreatorId equals b.UserId
                        where d.PostId == id
                        select new UserCreatedPost()
                        {
                            Tittle = d.Tittle,
                            CategoryId = d.CategoryId,
                            Content = d.Content,
                            CreatedDate = d.CreatedDate,
                            UserName = b.UserName,
                        }).FirstOrDefault();

            //Post post = blog.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Post/Create
        public ActionResult Create()
        {

            PostInsertModel obj = new PostInsertModel();

            obj.CategoryItems = new SelectList(blog.Categories, "CategoryId", "Name", 1);

            return View(obj);

        }

        // POST: /Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.       
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(PostInsertModel post)
        {

            if (ModelState.IsValid)
            {
                var postanc = new Post();
                postanc.Tittle = post.Tittle;
                postanc.Content = post.Content;
                postanc.CategoryId = post.SelectedCat;
                //postanc.CreatorId = ;
                postanc.CreatorId = post.CreatorId;
                postanc.CreatedDate = System.DateTime.Now;
                blog.Posts.Add(postanc);
                blog.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        // GET: /Post/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Post post = blog.Posts.Find(id);
            var post = (from d in blog.Posts
                        where d.PostId == id
                        select new PostInsertModel()
                        {
                            Tittle = d.Tittle,
                            CategoryId = d.CategoryId,
                            Content = d.Content,
                            SelectedCat = d.CategoryId,
                            //CreatorId = d.CreatorId   
                        }).FirstOrDefault();
            post.CategoryItems = new SelectList(blog.Categories, "CategoryId", "Name", 1);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Post/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostInsertModel post, int id)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(post).State = EntityState.Modified;
                var postanc = blog.Posts.Where(d => d.PostId == id).FirstOrDefault();
                postanc.PostId = id;
                postanc.Tittle = post.Tittle;
                postanc.Content = post.Content;
                postanc.CategoryId = post.SelectedCat;
                postanc.ModifiedDate = System.DateTime.Now;
                postanc.CreatorId = post.CreatorId;
                blog.Entry(postanc).State = EntityState.Modified;
                blog.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(post);
        }

        [HttpPost]
        public ActionResult Comment(UserCreatedPost comment)
        {

            var postanc = new Comment();
            
            postanc.CommentId = comment.CommentId;
            postanc.PostId = comment.PostId;
            postanc.Content = comment.contentcomment;
            postanc.UserId = comment.UserId;
            postanc.CreatedDate = System.DateTime.Now;
            blog.Comments.Add(postanc);
            blog.SaveChanges();

            //var comment = blog.Comments(postId, content);
            return RedirectToAction("Details");
        }

        // GET: /Post/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Post post = blog.Posts.Find(id);
        //    if (post == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(post);
        //}

        // POST: /Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = blog.Posts.Find(id);
            blog.Posts.Remove(post);
            blog.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }

}
