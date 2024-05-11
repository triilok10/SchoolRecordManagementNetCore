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

        public IActionResult Students()
        {
            string successMessage = TempData["SuccessMessage"] as string;
            ViewBag.SuccessMessage = successMessage;
            return View();
        }

        public IActionResult Teachers()
        {
            return View();
        }

        public IActionResult Notices()
        {
            return View();
        }
        public IActionResult Finance()
        {
            return View();
        }

        public IActionResult Attendance()
        {
            return View();
        }

        public IActionResult Classes()
        {
            return View();
        }

        public IActionResult Departments()
        {
            return View();
        }

        public IActionResult ChartsViewStudent()
        {
            return View();
        }
        public IActionResult ChartsViewTeacher()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
