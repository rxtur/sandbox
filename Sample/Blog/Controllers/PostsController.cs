using BlogiFire.Models;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BlogiFire.Controllers
{
    public class PostsController : Controller
    {
        #region Constructor and private memeber

        IPostRepository postsDb;
        IBlogRepository blogsDb;
        public PostsController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            this.postsDb = postsDb;
            this.blogsDb = blogsDb;
        }

        #endregion

        // GET: blog/posts
        [Route("blog/posts")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Blog posts ";

            var model = new PostViewModel();      
            model.Posts = await postsDb.All();

            if (User.Identity.IsAuthenticated)
            {
                var blogs = await blogsDb.Find(b => b.Author == User.Identity.Name);
                model.Blog = blogs.FirstOrDefault();
            }

            return View("~/Blog/Views/Posts/Index.cshtml", model);
        }

        // GET: blog/posts/my-post
        [Route("blog/posts/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var posts = await postsDb.Find(p => p.Slug == slug);
            var post = posts.FirstOrDefault();
            ViewBag.Title = post.Title;

            return View("~/Blog/Views/Posts/Single.cshtml", post);
        }
    }
}