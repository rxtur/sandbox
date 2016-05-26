using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Blog.Controllers
{
    [Route("{tenant}")]
    public class BlogsController : Controller
    {
        // GET: blogs/foo (post list)
        public IActionResult Index()
        {
            ViewBag.Title = "Blog list";
            return View("~/Blog/Views/Blogs/Index.cshtml");
        }

        // GET: blogs/foo/my-post (single post)
        [Route("{slug}")]
        public IActionResult Posts(string slug)
        {
            ViewBag.Title = "Post " + slug;
            return View("~/Blog/Views/Blogs/PostDetail.cshtml");
        }

    }
}