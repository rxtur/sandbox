using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Controllers
{
    [Route("blog/[controller]")]
    public class PostsController : Controller
    {
        IPostRepository db;
        public PostsController(IPostRepository db)
        {
            this.db = db;
        }
        // GET: blog/posts
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Blog posts ";

            var posts = await db.All();
            return View("~/Blog/Views/Posts/Index.cshtml", posts);
        }
    }
}