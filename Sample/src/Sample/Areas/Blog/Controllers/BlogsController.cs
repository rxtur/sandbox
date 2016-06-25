using Blogifier.Core.Infrastructure;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    [Route("blogs")]
    public class BlogsController : Controller
    {
        IPostRepository _postDb;
        IBlogRepository _blogDb;

        public BlogsController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            _postDb = postsDb;
            _blogDb = blogsDb;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All posts";
            var pagedList = await _postDb.Find(p => p.PostId > 0, 1, AppSettings.ItemsPerPage);
            return View("~/Areas/Blog/Views/PostsAll.cshtml", pagedList);
        }

        [Route("page/{page}")]
        public async Task<IActionResult> AllPaged(int page)
        {
            ViewBag.Title = "All posts";
            var pagedList = await _postDb.Find(p => p.PostId > 0, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Areas/Blog/Views/PostsAll.cshtml", pagedList);
        }

        [Route("{blog}")]
        public async Task<IActionResult> AuthorPosts(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, 1, AppSettings.ItemsPerPage);
            return View("~/Areas/Blog/Views/PostsByBlog.cshtml", pagedList);
        }

        [Route("{blog}/page/{page}")]
        public async Task<IActionResult> AuthorPostsPaged(string blog, int page)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Areas/Blog/Views/PostsByBlog.cshtml", pagedList);
        }

        [Route("{blog}/{slug}")]
        public IActionResult SinglePost(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Post " + slug;
            var item = _postDb.BySlug(slug);
            return View("~/Areas/Blog/Views/Single.cshtml", item);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}