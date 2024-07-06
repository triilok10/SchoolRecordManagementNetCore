using CoreProject1.Models;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace CoreProject1.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;
        private readonly dynamic _baseUrl;
        private readonly ILogger<HomeController> _logger;
        private readonly string _connectionString;

        public HomeController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, ILogger<HomeController> logger, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
            _logger = logger;
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host.Value}/"; _httpClient.BaseAddress = new Uri(_baseUrl);

        }

        #region DashBoardStudent""
        public JsonResult DashBoardStudent()
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

        #endregion

        #region "DashboardTeacher"
        public JsonResult DashboardTeacher()
        {
            List<TeacherDetail> lstTeacher = new List<TeacherDetail>();
            int maleTeacher = 0;
            int femaleTeacher = 0;
            int otherTeacher = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddViewTeachers", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            TeacherDetail objTeacher = new TeacherDetail();

                            if (Enum.TryParse<GenderTypes>(Convert.ToString(rdr["Gender"]), out GenderTypes gender))
                            {
                                objTeacher.Gender = gender;
                                if (gender == GenderTypes.Male)
                                {
                                    maleTeacher++;
                                }
                                else if (gender == GenderTypes.Female)
                                {
                                    femaleTeacher++;
                                }
                                else if (gender == GenderTypes.Other)
                                {
                                    otherTeacher++;
                                }
                            }

                            lstTeacher.Add(objTeacher);
                        }
                    }
                }
                int totalCount = maleTeacher + femaleTeacher + otherTeacher;

                if (totalCount == 0)
                {
                    return Json(new { error = "Total count of students is zero." });
                }

                double malePercentage = (double)maleTeacher / totalCount * 100;
                double femalePercentage = (double)femaleTeacher / totalCount * 100;
                double otherPercentage = (double)otherTeacher / totalCount * 100;

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

        #endregion


        #region "LibraryDashboard"
        public JsonResult LibraryDashboard()
        {
            List<Student> lstTeacher = new List<Student>();
            int HindiBook = 0;
            int EnglishBook = 0;
            int PunjabiBook = 0;
            int SpanishBook = 0;
            int ItalianBook = 0;
            int Other = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ViewBooks", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objLibrary = new Student();

                            if (Enum.TryParse<BookMedium>(Convert.ToString(rdr["Book1Medium"]), out BookMedium libMedium))
                            {
                                objLibrary.BookMediumLanguage = libMedium;
                                if (libMedium == BookMedium.English)
                                {
                                    EnglishBook++;
                                }
                                else if (libMedium == BookMedium.Hindi)
                                {
                                    HindiBook++;
                                }
                                else if (libMedium == BookMedium.Other)
                                {
                                    Other++;
                                }
                                else if (libMedium == BookMedium.Punjabi)
                                {
                                    PunjabiBook++;
                                }
                                else if (libMedium == BookMedium.Spanish)
                                {
                                    SpanishBook++;
                                }
                                else if (libMedium == BookMedium.Italian)
                                {
                                    ItalianBook++;
                                }
                            }

                            lstTeacher.Add(objLibrary);
                        }
                    }
                }
                int totalCount = EnglishBook + HindiBook + Other + PunjabiBook + SpanishBook + ItalianBook;

                if (totalCount == 0)
                {
                    return Json(new { error = "Total count of Books is zero." });
                }

                double EnglishBooks = (double)EnglishBook / totalCount * 100;
                double HindiBooks = (double)HindiBook / totalCount * 100;
                double PunjabiBooks = (double)PunjabiBook / totalCount * 100;
                double SpanishBooks = (double)SpanishBook / totalCount * 100;
                double ItalianBooks = (double)ItalianBook / totalCount * 100;
                double OtherBooks = (double)Other / totalCount * 100;

                var result = new
                {
                    EnglishBooks = EnglishBooks,
                    HindiBooks = HindiBooks,
                    PunjabiBooks = PunjabiBooks,
                    SpanishBooks = SpanishBooks,
                    ItalianBooks = ItalianBooks,
                    OtherBooks = OtherBooks
                };
                return Json(result);
            }

            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }

        }

        #endregion

        #region "Dashboard"

        public IActionResult Dashboard()
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

        #endregion

        #region "Students"

        [Route("Data-Students")]
        public async Task<IActionResult> Students()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                string apiurl = _baseUrl + "api/DataAPI/GetStudentData";
                HttpResponseMessage response = await _httpClient.GetAsync(apiurl);
                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    int maleCount = objstudent.Count(s => s.Gender == GenderType.Male);
                    int femaleCount = objstudent.Count(s => s.Gender == GenderType.Female);
                    int otherCount = objstudent.Count(s => s.Gender == GenderType.Other);
                    int totalCount = maleCount + femaleCount + otherCount;

                    ViewBag.MaleCount = maleCount;
                    ViewBag.FemaleCount = femaleCount;
                    ViewBag.OtherCount = otherCount;
                    ViewBag.totalCount = totalCount;
                    return View(objstudent);


                }


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

        #endregion


        #region "Teachers"
        [Route("Data-Teachers")]
        public async Task<IActionResult> Teachers()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");

            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                string apiurl = _baseUrl + "api/TeacherAPI/GetTeacherData";
                HttpResponseMessage response = await _httpClient.GetAsync(apiurl);
                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    int maleCount = objstudent.Count(s => s.Gender == GenderType.Male);
                    int femaleCount = objstudent.Count(s => s.Gender == GenderType.Female);
                    int otherCount = objstudent.Count(s => s.Gender == GenderType.Other);
                    int totalCount = maleCount + femaleCount + otherCount;

                    ViewBag.MaleCount = maleCount;
                    ViewBag.FemaleCount = femaleCount;
                    ViewBag.OtherCount = otherCount;
                    ViewBag.totalCount = totalCount;
                    return View(objstudent);


                }


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

        #endregion

        #region "Library Book Management"

        [Route("Library-Book-Management")]
        public async Task<IActionResult> LibraryBookManagement()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                string apiurl = _baseUrl + "api/LibraryAPI/GetLibraryData";
                HttpResponseMessage response = await _httpClient.GetAsync(apiurl);
                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objLibrary = JsonConvert.DeserializeObject<List<Student>>(responsebody);

                    int EnglishCount = objLibrary.Count(s => s.BookMediumLanguage == BookMedium.English);
                    int HindiCount = objLibrary.Count(s => s.BookMediumLanguage == BookMedium.Hindi);
                    int ItalianCount = objLibrary.Count(s => s.BookMediumLanguage == BookMedium.Italian);
                    int SpanishCount = objLibrary.Count(s => s.BookMediumLanguage == BookMedium.Spanish);
                    int PunjabiCount = objLibrary.Count(s => s.BookMediumLanguage == BookMedium.Punjabi);

                    ViewBag.EnglishCount = EnglishCount;
                    ViewBag.HindiCount = HindiCount;
                    ViewBag.ItalianCount = ItalianCount;
                    ViewBag.SpanishCount = SpanishCount;
                    ViewBag.PunjabiCount = PunjabiCount;
                    ViewBag.OtherCount = ItalianCount + SpanishCount + PunjabiCount;
                    ViewBag.TotalCount = EnglishCount + HindiCount + ItalianCount + SpanishCount + PunjabiCount;
                    return View();
                }

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


        #endregion

        #region "Classes"

        [Route("Classes")]
        public async Task<IActionResult> Classes(string Class)
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {

                if (!string.IsNullOrEmpty(Class))
                {
                    string apiUrl = $"{_baseUrl}api/LibraryAPI/GetClassRecord/{Class}";
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(responseBody);

                        return View(students);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View();
                }

            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }

        #endregion

        #region "FeeReport"
        public async Task<IActionResult> FeeReport(string Class)
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {

                if (!string.IsNullOrEmpty(Class))
                {
                    string apiUrl = $"{_baseUrl}api/LibraryAPI/GetClassRecord/{Class}";
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(responseBody);

                        return View(students);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {
                    return View();
                }

            }
            else
            {
                TempData["SuccessMessage"] = "Please login";
                return RedirectToAction("LogIn", "Log");
            }
        }


        #endregion

    }
}

