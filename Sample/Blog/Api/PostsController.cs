using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Api
{
    [Route("blog/api/[controller]")]
    [Authorize]
    public class PostsController : Controller
    {
        IPostRepository db;
        public PostsController(IPostRepository db)
        {
            this.db = db;
        }

        // GET: blog/api/posts
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Json(await db.All());
        }

        // GET: blog/api/posts/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await db.GetById(id));
        }

        // POST: blog/api/posts
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Post item)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

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

        // DELETE: blog/api/posts/2
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            await db.Delete(id);
            return "Deleted";
        }
    }
}