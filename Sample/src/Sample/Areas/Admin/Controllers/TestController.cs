using Microsoft.AspNetCore.Mvc;

namespace Sample.Controllers
{
    [Area("Admin")]
    [Route("admin")]
    public class TestController : Controller
    {
        [Route("test")]
        public IActionResult Index()
        {
            return View("~/Areas/Admin/Views/Test.cshtml");
        }
    }
}