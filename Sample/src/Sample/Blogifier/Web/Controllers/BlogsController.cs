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
            ViewBag.Title = "Blog list";
            var posts = await _postDb.All();
            return View("~/Views/Blogifier/PostList.cshtml", posts);
        }

        [Route("blogs/{blog}")]
        public async Task<IActionResult> AuthorPosts(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.Author = blog;
            var posts = await _postDb.Find(p => p.Blog.Slug == blog, 1, 10);

            return View("~/Views/Blogifier/AuthorPosts.cshtml", posts);
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
            ViewBag.Category = slug;

            var posts = await _postDb.ByCategory(slug, blog, 1, 10);

            return View("~/Views/Blogifier/CategoryPosts.cshtml", posts);
        }

        private bool BlogExists(string slug)
        {
            //var blogs = _blogDb.BlogsLookup();
            //if(blogs != null && blogs.Count > 0)
            //{
            //    foreach (var b in blogs)
            //    {
            //        if(b == slug)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;
            return _blogDb.BlogExists(slug);
        }
    }
}