using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Controllers
{
    [Route("blog/[controller]")]
    public class BlogsController : Controller
    {
        IBlogRepository db;
        public BlogsController(IBlogRepository db)
        {
            this.db = db;
        }
        // GET: blog/blogs
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Create new blog";

            var items = await db.All();
            return View("~/Blog/Views/Blogs/Index.cshtml", items);
        }

        // POST: blog/blogs
        [HttpPost]
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