using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace CoreProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
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

        public JsonResult ChartsViewStudent()
        {
            List<Student> ltrStudents = new List<Student>();
            int maleCount = 0;
            int femaleCount = 0;
            int otherCount = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddViewStudents", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();

                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                                if (gender == GenderType.Male)
                                {
                                    maleCount++;
                                }
                                else if (gender == GenderType.Female)
                                {
                                    femaleCount++;
                                }
                                else if (gender == GenderType.Other)
                                {
                                    otherCount++;
                                }
                            }

                            ltrStudents.Add(objStudent);
                        }
                    }
                }

                int totalCount = maleCount + femaleCount + otherCount;

                if (totalCount == 0)
                {
                    return Json(new { error = "Total count of students is zero." });
                }

                double malePercentage = (double)maleCount / totalCount * 100;
                double femalePercentage = (double)femaleCount / totalCount * 100;
                double otherPercentage = (double)otherCount / totalCount * 100;

                var result = new
                {
                    malePercentage = malePercentage,
                    femalePercentage = femalePercentage,
                    otherPercentage = otherPercentage
                };
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
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
