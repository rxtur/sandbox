using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.Infrastructure;
using Blogifier.Core.Models;
using Blogifier.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        IBlogRepository _blogDb;
        IPostRepository _postDb;

        public AdminController(IBlogRepository blogsDb, IPostRepository postsDb)
        {
            _blogDb = blogsDb;
            _postDb = postsDb;
        }

        public async Task<IActionResult> Index()
        {
            var blog = await _blogDb.ByIdentity(User.Identity.Name);
            if (blog == null)
            {
                var item = new Blog();
                item.IdentityName = User.Identity.Name;
                return View("~/Views/Blogifier/Admin/NewBlog.cshtml", item);
            }

            ViewBag.Title = "Admin";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog.Slug, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/Admin/Dashboard.cshtml", pagedList);
        }

        [Route("{blog}")]
        public async Task<IActionResult> Blog(string blog)
        {
            if (!BlogExists(blog))
            {
                var item = new Blog();
                item.IdentityName = blog;
                return View("~/Views/Blogifier/Admin/NewBlog.cshtml", item);
            }

            ViewBag.Title = "Admin";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/Admin/Dashboard.cshtml", pagedList);
        }

        [Route("{blog}/page/{page}")]
        public async Task<IActionResult> PostsPaged(string blog, int page)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin ";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Views/Blogifier/Admin/Dashboard.cshtml", pagedList);
        }

        [Route("{blog}/new")]
        public async Task<IActionResult> AdminNewPost(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            var item = new PostDetail();
            item.Post.Blog = await _blogDb.BySlug(blog);
            item.Post.BlogId = item.Post.Blog.BlogId;

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Admin/Editor.cshtml", item);
        }

        [Route("{blog}/profile")]
        public IActionResult AdminProfile(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Profile.cshtml");
        }

        [Route("{blog}/{slug}")]
        public IActionResult AdminEdit(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "AdminEdit";

            var item = _postDb.BySlug(slug);
            return View("~/Views/Blogifier/Admin/Editor.cshtml", item);
        }

        [HttpPost]
        [Route("{blog}/{slug}/save")]
        public async Task<ActionResult> PostSave(PostDetail model, string blog, string slug)
        {
            Core.Models.Post post;
            if(model.Post.PostId > 0)
            {
                post = await _postDb.Update(model.Post);
            }
            else
            {
                post = await _postDb.Add(model.Post);
            }
            var url = string.Format("~/{0}/{1}/{2}", Constants.Admin, blog, post.Slug);
            return Redirect(url);
        }

        [Route("{blog}/delete/{id}")]
        public async Task<ActionResult> PostDelete(string blog, int id)
        {
            await _postDb.Delete(id);
            var url = string.Format("~/{0}/{1}", Constants.Admin, blog);
            return Redirect(url);
        }

        [HttpPost]
        [Route("{blog}")]
        public async Task<ActionResult> BlogSave(Blog model, string blog)
        {
            Blog item = new Blog();
            if (model.BlogId > 0)
            {
                //item = await _postDb.Update(model.Post);
            }
            else
            {
                item = await _blogDb.Add(model);
            }
            var url = string.Format("~/{0}/{1}", Constants.Admin, item.Slug);
            return Redirect(url);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}
