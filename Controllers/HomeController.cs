using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
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
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }

        public IActionResult Dashboard()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if(IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
         
        }

        [Route("Data-Students")]
        public IActionResult Students()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                string successMessage = TempData["SuccessMessage"] as string;
                ViewBag.SuccessMessage = successMessage;
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }


        [Route("Data-Teachers")]
        public IActionResult Teachers()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");

            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                string successMessage = TempData["SuccessMessage"] as string;
                ViewBag.SuccessMessage = successMessage;
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }


        [Route("Library-Book-Management")]
        public IActionResult LibraryBookManagement()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }

        public IActionResult Classes()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if(IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                return View();
            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
