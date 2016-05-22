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



namespace Blog.Controllers
{
    public class PostController : Controller
    {
        private PostContext db = new PostContext();

        private BlogEntities blog = new BlogEntities();

        private IList<Post> allPost = new List<Post>();

        // GET: /Post/
        public ActionResult Index(int page = 1)
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
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;                  
            return View(list);
        }

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

        // GET: /Post/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
                      Post post = blog.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: /Post/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Post/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostId,Title,Content,CreatedDate,CreatorId")] Post post)
        {
            if (ModelState.IsValid)
            {
                blog.Posts.Add(post);
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
            Post post = blog.Posts.Find(id);
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
        public ActionResult Edit([Bind(Include = "PostId,Title,Content,CreatedDate,UserId")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: /Post/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = blog.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: /Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = blog.Posts.Find(id);
            blog.Posts.Remove(post);
            blog.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
