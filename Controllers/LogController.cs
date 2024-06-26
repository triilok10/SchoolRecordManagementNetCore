using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CoreProject1.Controllers
{
    public class LogController : Controller
    {
        public IActionResult LogIn(LogIn pLogin)
        {
            string Message = "";
            bool res = false;
            try
            {
                if (pLogin == null)
                {
                    Message = "Please Enter your Username and Password";
                    res = false;
                }
                if (pLogin.Username == null)
                {
                    Message = "Please Enter your Username";
                    res = false;
                }
                if (pLogin.Password == null)
                {
                    Message = "Please Enter your Password";
                    res = false;
                }


                if (pLogin != null)
                {
                    if (pLogin.Username == "admin" && pLogin.Password == "admin123")
                    {
                        res = true;
                        HttpContext.Session.SetString("IsLoggedIn", "true");
                        HttpContext.Session.SetString("Username", pLogin.Username);
                        HttpContext.Session.SetString("Password", pLogin.Password);
                       return RedirectToAction("Dashboard", "Home");

                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return View();
                }

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
          
        }
    }
}
