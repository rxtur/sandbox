using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blogifier.Core.Models;
using Blogifier.Core.ViewModels;
using Blogifier.Core.Infrastructure;
using Blogifier.Core.Repositories;
using Blogifier.Core.Repositories.Interfaces;

namespace Blogifier.Web.Controllers
{
    public class BlogsController : Controller
    {
        private BlogifierDbContext _context;
        IPostRepository _postDb;
        IBlogRepository _blogDb;
        ICategoryRepository _catDb;

        public BlogsController(BlogifierDbContext context, 
            IPostRepository postsDb, 
            IBlogRepository blogsDb, 
            ICategoryRepository catDb)
        {
            _context = context;
            _postDb = postsDb;
            _blogDb = blogsDb;
            _catDb = catDb;
        }

        [Route("blogs/test")]
        public async Task<IActionResult> Test()
        {
            ViewBag.Title = "Blog posts ";
            var posts = await _postDb.All();

            var x = posts.ToList();

            return View("~/Views/Blogifier/Profile.cshtml");
        }

        #region Blog routes

        [Route("blogs")]
        public async Task<IActionResult> Index()
        {
            ViewBag.Title = "Blog list";
            var posts = await _postDb.All();
            return View("~/Views/Blogifier/PostList.cshtml", posts);
        }

        [Route("blogs/{blog}")]
        public async Task<IActionResult> AuthorPosts(string blog)
        {
            if (!TenantExists(blog))
                return View("Error");

            ViewBag.Title = "Posts by " + blog;
            ViewBag.Author = blog;
            var posts = await _postDb.Find(p => p.Blog.Slug == blog, 1, 10);

            return View("~/Views/Blogifier/AuthorPosts.cshtml", posts);
        }

        [Route("blogs/{blog}/{slug}")]
        public async Task<IActionResult> SinglePost(string blog, string slug)
        {
            if (!TenantExists(blog))
                return View("Error");

            ViewBag.Title = "Post " + slug;
            var post = await _postDb.BySlug(slug);
            return View("~/Views/Blogifier/SinglePost.cshtml", post);
        }

        #endregion

        #region Categories and tags

        [Route("category/{blog}/{slug}")]
        public async Task<IActionResult> Category(string blog, string slug)
        {
            if (!TenantExists(blog))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.Category = slug;

            var posts = await _postDb.ByCategory(slug, blog, 1, 10);

            return View("~/Views/Blogifier/CategoryPosts.cshtml", posts);
        }

        [Route("tag/{tenant}/{name}")]
        public IActionResult Tag(string tenant, string name)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Tags";
            return View("~/Views/Blogifier/PostList.cshtml");
        }

        #endregion

        #region Admin routes

        [Route("admin/{tenant}/new")]
        public IActionResult AdminNew(string tenant)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/PostEditor.cshtml");
        }

        [Route("admin/{tenant}/profile")]
        public IActionResult AdminProfile(string tenant)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Profile.cshtml");
        }

        [Route("admin/{tenant}/{slug}")]
        public IActionResult AdminEdit(string tenant, string slug)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "AdminEdit";
            return View("~/Views/Blogifier/PostEditor.cshtml");
        }

        #endregion

        [Route("blogs/reset")]
        public IActionResult DbReset()
        {
            ViewBag.Title = "DB reset";

            // this is only to initiate database
            // and seed data for the very first load
            var setup = new Setup(_context); 
            setup.SeedData();

            return View("~/Views/Blogifier/Profile.cshtml");
        }

        private bool TenantExists(string tenant)
        {
            var blogs = _blogDb.BlogsLookup();
            if(blogs != null && blogs.Count > 0)
            {
                foreach (var b in blogs)
                {
                    if(b == tenant)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}