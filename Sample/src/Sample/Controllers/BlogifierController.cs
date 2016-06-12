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

namespace Blogifier.Controllers
{
    public class BlogsController : Controller
    {
        private BlogifierDbContext _context;
        IPostRepository _postDb;

        public BlogsController(BlogifierDbContext context, IPostRepository postsDb)
        {
            _context = context;
            _postDb = postsDb;
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

            //var posts = new PostListVM(_context);
            var posts = await _postDb.All();

            return View("~/Views/Blogifier/PostList.cshtml", posts);
        }

        [Route("blogs/{tenant}")]
        public IActionResult AuthorPosts(string tenant)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Posts by " + tenant;
            ViewBag.Author = tenant;
            return View("~/Views/Blogifier/AuthorPosts.cshtml");
        }

        [Route("blogs/{tenant}/{slug}")]
        public IActionResult SinglePost(string tenant, string slug)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Post " + slug;
            return View("~/Views/Blogifier/SinglePost.cshtml");
        }

        #endregion

        #region Categories and tags

        [Route("category/{tenant}/{name}")]
        public IActionResult Category(string tenant, string name)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.Category = name;
            return View("~/Views/Blogifier/CategoryPosts.cshtml");
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
            if (tenant == "bob")
                return true;
            if (tenant == "sam")
                return true;

            return false;
        }

    }
}