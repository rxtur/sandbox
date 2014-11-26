using System;
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
        #region Constructor and private members

        IPostRepository postsDb;
        IBlogRepository blogsDb;
        public PostsController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            this.postsDb = postsDb;
            this.blogsDb = blogsDb;
        }
        
        #endregion
        
        // GET: blog/api/posts
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Json(await postsDb.All());
        }

        // GET: blog/api/posts/2
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            return Json(await postsDb.GetById(id));
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

            try
            {
                item.Saved = DateTime.UtcNow;

                if (item.Id > 0)
                {
                    await postsDb.Update(item);
                }
                else
                {
                    var blogs = await blogsDb.Find(b => b.Author.ToLower() == User.Identity.Name);

                    if (blogs.Count > 0)
                    {
                        var blog = blogs.FirstOrDefault();
                        item.BlogId = blog.Id;
                    }

                    await postsDb.Add(item);
                    Context.Response.StatusCode = 201;
                }
                return new ObjectResult(item);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw;
            }


        }

        // DELETE: blog/api/posts/2
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            await postsDb.Delete(id);
            return "Deleted";
        }
    }
}