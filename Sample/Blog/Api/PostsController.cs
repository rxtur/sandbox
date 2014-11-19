using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using BlogiFire.Models;

namespace BlogiFire.Api
{
    [Route("blog/api/[controller]")]
    public class PostsController : Controller
    {
        public PostsController()
        {
        }

        // GET: blog/api/posts
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            using (var db = new BlogContext())
            {
                var posts = await db.Posts
                //.Include(p => p.Blog)
                .OrderBy(p => p.Title)
                .ToListAsync();
                return Json(posts);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreatePost([FromBody]Post post)
        {
            if (!ModelState.IsValid)
            {
                Context.Response.StatusCode = 400;
                return new ObjectResult("Model is invalid");
            }

            using (var db = new BlogContext())
            {
                await db.Posts.AddAsync(post);
                await db.SaveChangesAsync();

                Context.Response.StatusCode = 201;

                return new ObjectResult(post);
            }
        }
    }
}