using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;

namespace BlogiFire.Controllers
{
    [Route("blog/[controller]")]
    public class PostsController : Controller
    {
        // GET: blog/posts
        public IActionResult Index()
        {
            ViewBag.Title = "Blog posts";
            return View("~/Blog/Views/Posts/Index.cshtml");
        }
    }
}