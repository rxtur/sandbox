using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Blogifier.Controllers
{
    public class BlogsController : Controller
    {
        #region Blog routes

        [Route("blogs")]
        public IActionResult Index()
        {
            ViewBag.Title = "Blog list";
            return View("~/Views/Blogifier/PostList.cshtml");
        }

        [Route("blogs/{tenant}")]
        public IActionResult AuthorPosts(string tenant)
        {
            if (!TenantExists(tenant))
                return View("Error");

            ViewBag.Title = "Posts by " + tenant;
            return View("~/Views/Blogifier/PostList.cshtml");
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
            return View("~/Views/Blogifier/PostList.cshtml");
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