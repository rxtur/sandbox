using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Sample.Blog.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Admin";
            return View("~/Blog/Views/Admin/Index.cshtml");
        }

        // GET: admin/profile
        [Route("profile")]
        public IActionResult Profile()
        {
            ViewBag.Title = "Admin";
            return View("~/Blog/Views/Admin/Profile.cshtml");
        }


    }
}