using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Controllers
{
    [Route("blogs/{tenant}")]
    public class BlogsController : Controller
    {
        // GET: blogs/foo (post list)
        public IActionResult Index()
        {
            ViewBag.Title = "Blog list";
            return View("~/Views/Blogifier/Blogs/Index.cshtml");
        }

        // GET: blogs/foo/my-post (single post)
        [Route("{slug}")]
        public IActionResult Posts(string slug)
        {
            ViewBag.Title = "Post " + slug;
            return View("~/Views/Blogifier/Blogs/PostDetail.cshtml");
        }

        [Route("admin")]
        public IActionResult Admin()
        {
            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Admin/Index.cshtml");
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Admin/Profile.cshtml");
        }

    }
}