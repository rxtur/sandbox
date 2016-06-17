using Blogifier.Core.Infrastructure;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    public class BlogsController : Controller
    {
        IPostRepository _postDb;
        IBlogRepository _blogDb;
        ICategoryRepository _catDb;

        public BlogsController(IPostRepository postsDb, IBlogRepository blogsDb, ICategoryRepository catDb)
        {
            _postDb = postsDb;
            _blogDb = blogsDb;
            _catDb = catDb;
        }

        [Route("blogs")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "All posts";
            var pagedList = await _postDb.Find(p => p.PostId > 0, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/PostList.cshtml", pagedList);
        }

        [Route("blogs/page/{page}")]
        public async Task<IActionResult> AllPaged(int page)
        {
            ViewBag.Title = "All posts";
            var pagedList = await _postDb.Find(p => p.PostId > 0, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Views/Blogifier/PostList.cshtml", pagedList);
        }

        [Route("blogs/{blog}")]
        public async Task<IActionResult> AuthorPosts(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/AuthorPosts.cshtml", pagedList);
        }

        [Route("blogs/{blog}/page/{page}")]
        public async Task<IActionResult> AuthorPostsPaged(string blog, int page)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Views/Blogifier/AuthorPosts.cshtml", pagedList);
        }

        [Route("blogs/{blog}/{slug}")]
        public async Task<IActionResult> SinglePost(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Post " + slug;
            var post = await _postDb.BySlug(slug);
            return View("~/Views/Blogifier/SinglePost.cshtml", post);
        }

        [Route("category/{blog}/{slug}")]
        public async Task<IActionResult> Category(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.BlogSlug = blog;
            ViewBag.Category = slug;

            var pagedList = await _postDb.ByCategory(slug, blog, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/CategoryPosts.cshtml", pagedList);
        }

        [Route("category/{blog}/{slug}/page/{page}")]
        public async Task<IActionResult> PagedCategory(string blog, string slug, int page)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.BlogSlug = blog;
            ViewBag.Category = slug;

            var pagedList = await _postDb.ByCategory(slug, blog, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Views/Blogifier/CategoryPosts.cshtml", pagedList);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}