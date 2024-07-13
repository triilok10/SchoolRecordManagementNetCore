using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Web;

namespace CoreProject1.Controllers
{
    public class StudentUtilityController : Controller
    {
        private readonly HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;
        private readonly dynamic _baseUrl;

        public StudentUtilityController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host.Value}/"; _httpClient.BaseAddress = new Uri(_baseUrl);

        }


        #region "ViewStudent"
        [HttpGet]
        [Route("View-Student-Record")]
        public async Task<IActionResult> ViewStudent()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/DataAPI/ViewStudentAPI");
                    if (response.IsSuccessStatusCode)
                    {
                        string responsebody = await response.Content.ReadAsStringAsync();
                        List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                        return View(objstudent);
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Please login to view the Students";
                return RedirectToAction("LogIn", "Log");
            }
        }

        #endregion

        #region "AddStudent"

        [Route("Add-Student-Record")]
        public IActionResult AddStudent()
        {
            return View(new Student());
        }


        [HttpPost]
        [Route("Add-NewStudent-Record")]
        public async Task<IActionResult> CreateStudentData(Student pStudent, IFormFile File)
        {
            bool res = false;
            string successMessage = "";
            try
            {


                if (pStudent.FirstName != "" && pStudent.FatherName != "" && pStudent.Email != "")
                {

                    if (File != null && File.Length > 0)
                    {
                        string FileName = Path.GetFileName(File.FileName);
                        string FilePathData = Path.Combine("wwwroot", "Images", "UserImages", FileName);
                        using (var stream = new FileStream(FilePathData, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }

                        pStudent.Filepath = FileName;
                    }
                    if (pStudent.DateOfBirth < new DateTime(1998, 1, 1))
                    {
                        TempData["SuccessMessage"] = "Please Enter the Correct DOB.";
                        return View("AddStudent");
                    }
                    if (pStudent.FirstName == pStudent.FatherName)
                    {
                        TempData["SuccessMessage"] = "Student Name and Father's Name not be Same! , Please fill the correct data.";
                        return View("AddStudent");
                    }
                    if (pStudent.Mobile.Length != 10)
                    {
                        TempData["SuccessMessage"] = "Please Enter the 10 digit Mobile Number.";
                        return View("AddStudent");
                    }



                    string Apiurl = _baseUrl + "api/DataAPI/AddStudentAPI";
                    string FullUrl = $"{Apiurl}?FirstName={(string.IsNullOrWhiteSpace(pStudent.FirstName) ? "" : HttpUtility.UrlEncode(pStudent.FirstName))}" +
                       $"&LastName={(string.IsNullOrWhiteSpace(pStudent.LastName) ? "" : HttpUtility.UrlEncode(pStudent.LastName))}" +
                       $"&FatherName={(string.IsNullOrWhiteSpace(pStudent.FatherName) ? "" : HttpUtility.UrlEncode(pStudent.FatherName))}" +
                       $"&MotherName={(string.IsNullOrWhiteSpace(pStudent.MotherName) ? "" : HttpUtility.UrlEncode(pStudent.MotherName))}" +
                       $"&Mobile={(string.IsNullOrWhiteSpace(pStudent.Mobile) ? "" : HttpUtility.UrlEncode(pStudent.Mobile))}" +
                       $"&Gender={(string.IsNullOrWhiteSpace(pStudent.Gender.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Gender.ToString()))}" +
                       $"&Email={(string.IsNullOrWhiteSpace(pStudent.Email) ? "" : HttpUtility.UrlEncode(pStudent.Email))}" +
                       $"&Remarks={(string.IsNullOrWhiteSpace(pStudent.Remarks) ? "" : HttpUtility.UrlEncode(pStudent.Remarks))}" +
                       $"&Class={(string.IsNullOrWhiteSpace(pStudent.Class.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Class.ToString()))}" +
                       $"&DateOfBirth={(string.IsNullOrWhiteSpace(pStudent.DateOfBirth.ToString()) ? "DD/MM/YYYY" : HttpUtility.UrlEncode(pStudent.DateOfBirth.ToString()))}" +
                       $"&Filepath={(string.IsNullOrWhiteSpace(pStudent.Filepath) ? "Null.jpg" : HttpUtility.UrlEncode(pStudent.Filepath))}" +
                       $"&Address={(string.IsNullOrWhiteSpace(pStudent.Address) ? "" : HttpUtility.UrlEncode(pStudent.Address))}";

                    HttpResponseMessage response = await _httpClient.GetAsync(FullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        successMessage = responseObject.message;
                        ModelState.Clear();
                        TempData["SuccessMessage"] = successMessage;
                        return View("AddStudent");
                    }
                    else
                    {

                        TempData["SuccessMessage"] = "Please Enter the Correct Data, Failed!";
                        return View("AddStudent");
                    }
                }
                else
                {
                    TempData["SuccessMessage"] = "Please Enter the Mandatory Field's.";
                    return View("AddStudent");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        #endregion

        #region "Update Student"
        [HttpGet]
        [Route("ViewUpdate-Student-Record")]
        public async Task<IActionResult> UpdateStudent()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/DataAPI/ViewStudentAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    return View(objstudent);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("PostUpdate-Student-Record")]
        public async Task<IActionResult> UpdateChangeData(int Id)
        {
            try
            {
                string apiUrl = $"{_baseUrl}api/DataAPI/UpdateChangeDataAPI/{Id}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();

                    Student student = JsonConvert.DeserializeObject<Student>(responsebody);
                    ViewBag.hdngenderValue = student.Gender;
                    ViewBag.hdnclassValue = student.Class;
                    ViewBag.hdnDateOfBirth = student.DateOfBirth;
                    return View(student);

                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> UpdateStudentData(Student pStudent, IFormFile File)
        {

            try
            {
                if (pStudent.FirstName != "" && pStudent.FatherName != "" && pStudent.Email != "")
                {

                    if (File != null && File.Length > 0)
                    {
                        string FileName = Path.GetFileName(File.FileName);
                        string FilePathData = Path.Combine("wwwroot", "Images", "UserImages", FileName);
                        using (var stream = new FileStream(FilePathData, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }

                        pStudent.Filepath = FileName;
                    }
                    if (string.IsNullOrWhiteSpace(pStudent.Filepath))
                    {
                        pStudent.Filepath = "null.jpg";
                    }
                    if (pStudent.DateOfBirth < new DateTime(1998, 1, 1))
                    {
                        TempData["SuccessMessage"] = "Please Enter the Correct DOB.";
                        return RedirectToAction("UpdateChangeData");
                    }
                    if (pStudent.FirstName == pStudent.FatherName)
                    {
                        TempData["SuccessMessage"] = "Student Name and Father's Name not be Same!, Please fill the correct data.";
                        return RedirectToAction("UpdateChangeData");
                    }
                    if (pStudent.Mobile.Length != 10)
                    {
                        TempData["SuccessMessage"] = "Please Enter the 10 digit Mobile Number.";
                        return RedirectToAction("UpdateChangeData");
                    }
                    string Apiurl = _baseUrl + "api/DataAPI/UpdateStudentDataAPI";


                    string FullUrl = $"{Apiurl}?FirstName={(string.IsNullOrWhiteSpace(pStudent.FirstName) ? "" : HttpUtility.UrlEncode(pStudent.FirstName))}" +
                        $"&Id={(string.IsNullOrWhiteSpace(pStudent.Id.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Id.ToString()))}" +
                        $"&LastName={(string.IsNullOrWhiteSpace(pStudent.LastName) ? "" : HttpUtility.UrlEncode(pStudent.LastName))}" +
                        $"&FatherName={(string.IsNullOrWhiteSpace(pStudent.FatherName) ? "" : HttpUtility.UrlEncode(pStudent.FatherName))}" +
                        $"&MotherName={(string.IsNullOrWhiteSpace(pStudent.MotherName) ? "" : HttpUtility.UrlEncode(pStudent.MotherName))}" +
                        $"&Mobile={(string.IsNullOrWhiteSpace(pStudent.Mobile) ? "" : HttpUtility.UrlEncode(pStudent.Mobile))}" +
                        $"&Gender={(string.IsNullOrWhiteSpace(pStudent.hdnGender) ? "" : HttpUtility.UrlEncode(pStudent.hdnGender))}" +
                        $"&Email={(string.IsNullOrWhiteSpace(pStudent.Email) ? "" : HttpUtility.UrlEncode(pStudent.Email))}" +
                        $"&Remarks={(string.IsNullOrWhiteSpace(pStudent.Remarks) ? "" : HttpUtility.UrlEncode(pStudent.Remarks))}" +
                        $"&Class={(string.IsNullOrWhiteSpace(pStudent.hdnClass) ? "" : HttpUtility.UrlEncode(pStudent.hdnClass))}" +
                        $"&DateOfBirth={(string.IsNullOrWhiteSpace(pStudent.DateOfBirth.ToString()) ? "DD/MM/YYYY" : HttpUtility.UrlEncode(pStudent.DateOfBirth.ToString()))}" +
                        $"&Filepath={(string.IsNullOrWhiteSpace(pStudent.Filepath) ? "Null.jpg" : HttpUtility.UrlEncode(pStudent.Filepath))}" +
                        $"&Address={(string.IsNullOrWhiteSpace(pStudent.Address) ? "" : HttpUtility.UrlEncode(pStudent.Address))}";

                    HttpResponseMessage response = await _httpClient.GetAsync(FullUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        string successMessage = responseObject.message;

                        ModelState.Clear();
                        TempData["SuccessMessage"] = successMessage;
                        return RedirectToAction("UpdateStudent");
                    }
                    else
                    {
                        return View("Error");
                    }
                }
                else
                {

                    return View("Error");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }
        #endregion

        #region "Delete Student"

        [HttpGet]
        [Route("DeleteView-Student-Record")]
        public async Task<IActionResult> DeleteStudent()
        {
            try
            {
                string successMessage = TempData["SuccessMessage"] as string;
                if (!string.IsNullOrEmpty(successMessage))
                {
                    ViewBag.SuccessMessage = successMessage;
                }

                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/DataAPI/ViewStudentAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    return View(objstudent);
                }
                else
                {
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Route("Delete-Student-Record")]
        public async Task<IActionResult> DeleteStudentData(int Id)
        {
            try
            {
                string apiUrl = $"{_baseUrl}api/DataAPI/DeleteStudentAPI/{Id}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responsebody);
                    string message = responseObject.message;

                    if (message == "Please pay the dues to delete the record of the student.")
                    {
                        TempData["ErrorMessage"] = message;
                        TempData.Keep("ErrorMessage");
                    }
                    else if(message == "Please submit the library book to delete the record of the student.")
                    {
                        TempData["ErrorMessage"] = message;
                        TempData.Keep("ErrorMessage");
                    }
                    else
                    {
                        TempData["SuccessMessage"] = message;
                        TempData.Keep("SuccessMessage");
                    }

                    return RedirectToAction("DeleteStudent");

                }
                else
                {

                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
        #endregion
    }
}
