using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.Infrastructure;
using Blogifier.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    public class AdminController : Controller
    {
        IBlogRepository _blogDb;
        IPostRepository _postDb;

        public AdminController(IBlogRepository blogsDb, IPostRepository postsDb)
        {
            _blogDb = blogsDb;
            _postDb = postsDb;
        }

        [Route("admin/{blog}")]
        public async Task<IActionResult> Index(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/Admin/Dashboard.cshtml", pagedList);
        }

        [Route("admin/{blog}/page/{page}")]
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

        [Route("admin/{blog}/new")]
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

        [Route("admin/{blog}/profile")]
        public IActionResult AdminProfile(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Profile.cshtml");
        }

        [Route("admin/{blog}/{slug}")]
        public IActionResult AdminEdit(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "AdminEdit";

            var item = _postDb.BySlug(slug);
            return View("~/Views/Blogifier/Admin/Editor.cshtml", item);
        }

        [HttpPost]
        [Route("admin/{blog}/{slug}/save")]
        public async Task<ActionResult> PostSave(PostDetail model, string blog, string slug)
        {
            if(model.Post.PostId > 0)
            {
                await _postDb.Update(model.Post);
            }
            else
            {
                await _postDb.Add(model.Post);
            }
            var item = _postDb.BySlug(slug);
            return View("~/Views/Blogifier/Admin/Editor.cshtml", item);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}
