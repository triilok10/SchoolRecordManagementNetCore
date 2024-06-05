using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CoreProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        [Route("Data-Students")]
        public IActionResult Students()
        {
            string successMessage = TempData["SuccessMessage"] as string;
            ViewBag.SuccessMessage = successMessage;
            return View();
        }


        [Route("Data-Teachers")]
        public IActionResult Teachers()
        {
            string successMessage = TempData["SuccessMessage"] as string;
            ViewBag.SuccessMessage = successMessage;
            return View();
        }


        [Route("Library-Book-Management")]
        public IActionResult LibraryBookManagement()
        {
            return View();
        }
    
        public IActionResult Classes()
        {
            return View();
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
