using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Api
{
    [Route("blog/api/[controller]")]
    public class AuthorController : Controller
    {
        IBlogRepository db;
        public AuthorController(IBlogRepository db)
        {
            this.db = db;
        }

        // GET: blog/api/author
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var author = new Author();

            author.Name = User.Identity.Name;
            author.IsAuthenticated = User.Identity.IsAuthenticated;

            if (!string.IsNullOrEmpty(author.Name))
            {
                var blog = await db.Find(b => b.Author.ToLower() == author.Name.ToLower());
                if(blog != null && blog.Count > 0)
                {
                    author.HasBlog = true;
                }
            }

            return Json(author);
        }
    }
}