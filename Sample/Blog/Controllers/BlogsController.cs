using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Controllers
{
    public class BlogsController : Controller
    {
        IBlogRepository db;
        public BlogsController(IBlogRepository db)
        {
            this.db = db;
        }

        [Route("blog/new")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "New blog";

            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/account/login");
            }

            var blogs = await db.Find(b => b.AuthorId == User.Identity.Name);
            var blog = blogs.FirstOrDefault();

            if(blog == null)
            {
                blog = new Blog();
                blog.AuthorId = User.Identity.Name;
                blog.AuthorName = User.Identity.Name;
            }

            return View("~/Blog/Views/Blogs/New.cshtml", blog);
        }

        // POST: blog/create
        [Route("blog/create")]
        public async Task<ActionResult> Post([FromBody]Blog item)
        {
            if (item.Id > 0)
            {
                await db.Update(item);
            }
            else
            {
                await db.Add(item);

                Context.Response.StatusCode = 201;
            }
            return new ObjectResult(item);
        }

    }
}