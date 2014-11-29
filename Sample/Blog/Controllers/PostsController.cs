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
        int pageSize;
        public PostsController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            this.postsDb = postsDb;
            this.blogsDb = blogsDb;
            pageSize = 10;
        }

        #endregion

        // GET: blog
        [Route("blog")]
        public async Task<IActionResult> Index()
        {
            int page = 1;
            ViewBag.Title = "Blog posts ";    
            var posts = await postsDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = 0;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts);
        }

        // GET: blog/page/2
        [Route("blog/page/{page:int}")]
        public async Task<IActionResult> Page(int page)
        {
            ViewBag.Title = "Blog posts ";
            var posts = await postsDb.Find(p => p.Visible == true, page, pageSize);

            ViewBag.NewerPage = page - 1;
            await GetOlderPage(page);

            return View("~/Blog/Views/Posts/Index.cshtml", posts);
        }

        // GET: blog/post/my-post
        [Route("blog/post/{slug}")]
        public async Task<IActionResult> Single(string slug)
        {
            var posts = await postsDb.Find(p => p.Slug == slug);
            var post = posts.FirstOrDefault();
            ViewBag.Title = post.Title;

            return View("~/Blog/Views/Posts/Post.cshtml", post);
        }

        // get page number by quering if older records exist
        private async Task GetOlderPage(int page)
        {
            ViewBag.OlderPage = page + 1;
            var olderPosts = await postsDb.Find(p => p.Visible == true, page + 1, pageSize);
            if (olderPosts == null || olderPosts.Count() < 1)
            {
                ViewBag.OlderPage = 0;
            }
        }
    }
}