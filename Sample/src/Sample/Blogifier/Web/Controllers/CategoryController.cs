using Blogifier.Core.Infrastructure;
using Blogifier.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Blogifier.Web.Controllers
{
    [Route("category")]
    public class CategoryController : Controller
    {
        IPostRepository _postDb;
        IBlogRepository _blogDb;

        public CategoryController(IPostRepository postsDb, IBlogRepository blogsDb)
        {
            _postDb = postsDb;
            _blogDb = blogsDb;
        }

        [Route("{blog}/{slug}")]
        public async Task<IActionResult> Category(string blog, string slug)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.BlogSlug = blog;
            ViewBag.Category = slug;

            var pagedList = await _postDb.ByCategory(slug, blog, 1, AppSettings.ItemsPerPage);
            return View("~/Views/Blogifier/Posts/ByCategory.cshtml", pagedList);
        }

        [Route("{blog}/{slug}/page/{page}")]
        public async Task<IActionResult> PagedCategory(string blog, string slug, int page)
        {
            if (!BlogExists(blog))
                return View("Error");

            ViewBag.Title = "Categories";
            ViewBag.BlogSlug = blog;
            ViewBag.Category = slug;

            var pagedList = await _postDb.ByCategory(slug, blog, page, AppSettings.ItemsPerPage);

            if (pagedList.Pager.RedirectToError)
                return View("Error");

            return View("~/Views/Blogifier/Posts/ByCategory.cshtml", pagedList);
        }

        private bool BlogExists(string slug)
        {
            return _blogDb.BlogExists(slug);
        }
    }
}