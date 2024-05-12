using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CoreProject1.Controllers
{
    public class TeacherUtilityController : Controller
    {
        private readonly HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;
        private readonly dynamic _baseUrl;

        public TeacherUtilityController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host.Value}/"; _httpClient.BaseAddress = new Uri(_baseUrl);

        }

        #region "View Teacher"

        [HttpGet]
        [Route("View-Teacher")]
        public async Task<IActionResult> ViewTeacher()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/TeacherAPI/ViewTeacherAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<TeacherDetail> objstudent = JsonConvert.DeserializeObject<List<TeacherDetail>>(responsebody);
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


        #endregion

        #region "Add Teacher"

        [Route("Add-Teacher-Record")]
        public IActionResult AddTeacher()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherData(TeacherDetail pStudent, IFormFile File)
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

                    if (string.IsNullOrEmpty(pStudent.Filepath))
                    {
                        pStudent.Filepath = "";
                    }
                    if (string.IsNullOrEmpty(pStudent.LastName))
                    {
                        pStudent.LastName = "";
                    }
                    if (pStudent.FirstName == pStudent.FatherName)
                    {
                        ViewBag.SuccessMessage = "Student Name and Father's Name not be Same!, Please fill the correct data.";
                        return View("AddTeacher");
                    }

                    string Apiurl = _baseUrl + "api/TeacherAPI/AddTeacherAPI";


                    string jsonStudent = JsonConvert.SerializeObject(pStudent);


                    var content = new StringContent(jsonStudent, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await _httpClient.PostAsync(Apiurl, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        string successMessage = responseObject.message;


                        ViewBag.SuccessMessage = successMessage;
                        return View("AddTeacher");
                    }
                    else
                    {
                        ViewBag.SuccessMessage = "Please Enter the Correct Data, Failed!";
                        return View("AddTeacher");
                    }
                }
                else
                {
                    ViewBag.SuccessMessage = "Please Enter the Mandatory Field's.";
                    return View("AddTeacher");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        #endregion

        #region "Update Teacher"
        [HttpGet]
        [Route("ViewUpdate-Teacher-Record")]
        public async Task<IActionResult> UpdateTeacher()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/TeacherAPI/ViewTeacherAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<TeacherDetail> objstudent = JsonConvert.DeserializeObject<List<TeacherDetail>>(responsebody);
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
        [Route("PostUpdate-Teacher-Record")]
        public async Task<IActionResult> UpdateChangeData(int Id)
        {
            try
            {
                string apiUrl = $"{_baseUrl}api/TeacherAPI/UpdateChangeDataAPI/{Id}";
                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();

                    TeacherDetail student = JsonConvert.DeserializeObject<TeacherDetail>(responsebody);
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
        public async Task<IActionResult> UpdateTeacherData(TeacherDetail pStudent, IFormFile File)
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
                    if (string.IsNullOrEmpty(pStudent.LastName))
                    {
                        pStudent.LastName = "";
                    }
                    if (pStudent.FirstName == pStudent.FatherName)
                    {
                        ViewBag.SuccessMessage = "Student Name and Father's Name not be Same!, Please fill the correct data.";
                        return View("UpdateChangeData");
                    }
                    if (pStudent.Mobile.Length != 10)
                    {
                        ViewBag.SuccessMessage = "Please Enter the 10 digit Mobile Number.";
                        return View("UpdateChangeData");
                    }
                    string Apiurl = _baseUrl + "api/TeacherAPI/UpdateTeacherDataAPI";


                    string jsonStudent = JsonConvert.SerializeObject(pStudent);


                    var content = new StringContent(jsonStudent, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await _httpClient.PostAsync(Apiurl, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        string successMessage = responseObject.message;


                        ViewBag.SuccessMessage = successMessage;
                        return View("UpdateChangeData");
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
        [Route("DeleteView-Teacher-Record")]
        public async Task<IActionResult> DeleteTeacher()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/TeacherAPI/ViewTeacherAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<TeacherDetail> objstudent = JsonConvert.DeserializeObject<List<TeacherDetail>>(responsebody);
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
        [Route("Delete-Teacher-Record")]
        public async Task<IActionResult> DeleteTeacherData(int Id)
        {
            try
            {
                string apiUrl = $"{_baseUrl}api/TeacherAPI/DeleteTeacherAPI/{Id}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responsebody);
                    string message = responseObject.message;

                    TempData["SuccessMessage"] = message;

                    return RedirectToAction("Teachers", "Home");

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
