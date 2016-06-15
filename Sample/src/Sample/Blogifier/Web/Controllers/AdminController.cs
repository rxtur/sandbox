using Blogifier.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    public class AdminController : Controller
    {
        IBlogRepository _blogDb;

        public AdminController(IBlogRepository blogsDb)
        {
            _blogDb = blogsDb;
        }

        [Route("admin/{blog}/new")]
        public IActionResult AdminNew(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/PostEditor.cshtml");
        }

        [Route("admin/{blog}/profile")]
        public IActionResult AdminProfile(string blog)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Admin";
            return View("~/Views/Blogifier/Profile.cshtml");
        }

        [Route("admin/{blog}/{slug}")]
        public IActionResult AdminEdit(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "AdminEdit";
            return View("~/Views/Blogifier/PostEditor.cshtml");
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}
