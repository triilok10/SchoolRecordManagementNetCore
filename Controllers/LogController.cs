using Microsoft.AspNetCore.Mvc;

namespace CoreProject1.Controllers
{
    public class LogController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
