using Blogifier.Core.Infrastructure;
using Blogifier.Core.Models;
using Blogifier.Core.Repositories.Interfaces;
using Blogifier.Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    [Authorize]
    [Area("Admin")]
    [Route("admin")]
    public class AdminController : Controller
    {
        IBlogRepository _blogDb;
        IPostRepository _postDb;

        public AdminController(IBlogRepository blogsDb, IPostRepository postsDb)
        {
            _blogDb = blogsDb;
            _postDb = postsDb;
        }

        public async Task<IActionResult> Index()
        {
            var blog = GetUserBlog();
            if (blog == null)
            {
                blog = new Blog();
                blog.IdentityName = User.Identity.Name;
                return View("~/Areas/Admin/Views/Profile.cshtml", blog);
            }

            ViewBag.Title = "Admin";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog.Slug, 1, AppSettings.ItemsPerPage);
            return View("~/Areas/Admin/Views/Index.cshtml", pagedList);
        }

        [Route("page/{page}")]
        public async Task<IActionResult> Index(int page)
        {
            var blog = GetUserBlog();
            if (blog == null)
                return View("Error");

            ViewBag.Title = "Admin ";
            ViewBag.BlogSlug = blog;

            var pagedList = await _postDb.Find(p => p.Blog.Slug == blog.Slug, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Areas/Admin/Views/Index.cshtml", pagedList);
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            ViewBag.Title = "Profile";
            var blog = GetUserBlog();
            if (blog == null)
            {
                blog = new Blog();
                blog.IdentityName = User.Identity.Name;
            }
            return View("~/Areas/Admin/Views/Profile.cshtml", blog);
        }

        [HttpPost]
        [Route("profile")]
        public async Task<ActionResult> Profile(Blog model)
        {
            Blog item = new Blog();
            if (model.BlogId > 0)
            {
                item = await _blogDb.Update(model);
            }
            else
            {
                item = await _blogDb.Add(model);
            }
            return View("~/Areas/Admin/Views/Profile.cshtml", item);
        }

        [Route("editor")]
        public IActionResult Editor()
        {
            var item = new PostDetail();
            var blog = GetUserBlog();
            if (blog == null)
            {
                var url = string.Format("~/{0}/profile", Constants.Admin);
                return Redirect(url);
            }
            item.Blog = blog;
            return View("~/Areas/Admin/Views/Editor.cshtml", item);
        }

        [Route("editor/{slug}")]
        public IActionResult Editor(string slug)
        {
            var item = _postDb.BySlug(slug);
            var blog = GetUserBlog();
            if (blog == null)
            {
                var url = string.Format("~/{0}/profile", Constants.Admin);
                return Redirect(url);
            }
            item.Blog = blog;
            return View("~/Areas/Admin/Views/Editor.cshtml", item);
        }

        [HttpPost]
        [Route("editor")]
        public async Task<ActionResult> Editor(PostDetail model)
        {
            Post post;
            if(model.Post.PostId > 0)
            {
                post = await _postDb.Update(model.Post);
            }
            else
            {
                var blog = GetUserBlog();
                if (blog == null)
                {
                    return Redirect(string.Format("~/{0}/profile", Constants.Admin));
                }
                model.Post.BlogId = blog.BlogId;
                post = await _postDb.Add(model.Post);
            }
            var url = string.Format("~/{0}/editor/{1}", Constants.Admin, post.Slug);
            return Redirect(url);
        }

        [Route("{blog}/delete/{id}")]
        public async Task<ActionResult> PostDelete(string blog, int id)
        {
            await _postDb.Delete(id);
            var url = string.Format("~/{0}/{1}", Constants.Admin, blog);
            return Redirect(url);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }

        private Blog GetUserBlog()
        {
            //TODO: use cache
            var blog = _blogDb.ByIdentity(User.Identity.Name);
            return blog.Result;
        }
    }
}
