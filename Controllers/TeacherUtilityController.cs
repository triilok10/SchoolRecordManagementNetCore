using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Web;

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
            return View(new TeacherDetail());
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeacherData(TeacherDetail pTeacher, IFormFile File)
        {
            try
            {
                if (pTeacher.FirstName != "" && pTeacher.FatherName != "" && pTeacher.Email != "")
                {

                    if (File != null && File.Length > 0)
                    {
                        string FileName = Path.GetFileName(File.FileName);
                        string FilePathData = Path.Combine("wwwroot", "Images", "UserImages", FileName);
                        using (var stream = new FileStream(FilePathData, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }

                        pTeacher.Filepath = FileName;
                    }

                    if (string.IsNullOrEmpty(pTeacher.Filepath))
                    {
                        pTeacher.Filepath = "";
                    }
                    if (string.IsNullOrEmpty(pTeacher.LastName))
                    {
                        pTeacher.LastName = "";
                    }
                    if (pTeacher.DateOfBirth < new DateTime(1998, 1, 1))
                    {
                        TempData["SuccessMessage"] = "Please Enter the Correct DOB.";
                        return View("UpdateChangeData");
                    }
                    if (pTeacher.FirstName == pTeacher.FatherName)
                    {
                        TempData["SuccessMessage"] = "Student Name and Father's Name not be Same!, Please fill the correct data.";
                        return View("AddTeacher");
                    }

                    string Apiurl = _baseUrl + "api/TeacherAPI/AddTeacherAPI";


                    string fullurl = $"{Apiurl}?FirstName={(string.IsNullOrWhiteSpace(pTeacher.FirstName) ? "" : HttpUtility.UrlEncode(pTeacher.FirstName))}+" +
                        $"&LastName{(string.IsNullOrWhiteSpace(pTeacher.LastName) ? "" : HttpUtility.UrlEncode(pTeacher.LastName))}+" +
                        $"&FatherName={(string.IsNullOrWhiteSpace(pTeacher.FatherName) ? "" : HttpUtility.UrlEncode(pTeacher.FatherName))}" +
                       $"&MotherName={(string.IsNullOrWhiteSpace(pTeacher.MotherName) ? "" : HttpUtility.UrlEncode(pTeacher.MotherName))}" +
                       $"&Mobile={(string.IsNullOrWhiteSpace(pTeacher.Mobile) ? "" : HttpUtility.UrlEncode(pTeacher.Mobile))}" +
                       $"&Gender={(string.IsNullOrWhiteSpace(pTeacher.Gender.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.Gender.ToString()))}" +
                       $"&Email={(string.IsNullOrWhiteSpace(pTeacher.Email) ? "" : HttpUtility.UrlEncode(pTeacher.Email))}" +
                       $"&Remarks={(string.IsNullOrWhiteSpace(pTeacher.Remarks) ? "" : HttpUtility.UrlEncode(pTeacher.Remarks))}" +
                       $"&Subject={(string.IsNullOrWhiteSpace(pTeacher.Subject.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.Subject.ToString()))}" +
                       $"&DateOfBirth={(string.IsNullOrWhiteSpace(pTeacher.DateOfBirth.ToString()) ? "DD/MM/YYYY" : HttpUtility.UrlEncode(pTeacher.DateOfBirth.ToString()))}" +
                       $"&Filepath={(string.IsNullOrWhiteSpace(pTeacher.Filepath) ? "Null.jpg" : HttpUtility.UrlEncode(pTeacher.Filepath))}" +
                       $"&Address={(string.IsNullOrWhiteSpace(pTeacher.Address) ? "" : HttpUtility.UrlEncode(pTeacher.Address))}";

                    HttpResponseMessage response = await _httpClient.GetAsync(fullurl);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        string successMessage = responseObject.message;
                        ModelState.Clear();
                        TempData["SuccessMessage"] = successMessage;
                        return View("AddTeacher");
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Please Enter the Correct Data, Failed!";
                        return View("AddTeacher");
                    }
                }
                else
                {
                    TempData["SuccessMessage"] = "Please Enter the Mandatory Field's.";
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
        public async Task<IActionResult> UpdateTeacherData(TeacherDetail pTeacher, IFormFile File)
        {
            try
            {
                if (pTeacher.FirstName != "" && pTeacher.FatherName != "" && pTeacher.Email != "")
                {

                    if (File != null && File.Length > 0)
                    {
                        string FileName = Path.GetFileName(File.FileName);
                        string FilePathData = Path.Combine("wwwroot", "Images", "UserImages", FileName);
                        using (var stream = new FileStream(FilePathData, FileMode.Create))
                        {
                            await File.CopyToAsync(stream);
                        }

                        pTeacher.Filepath = FileName;
                    }
                    if (string.IsNullOrWhiteSpace(pTeacher.Filepath))
                    {
                        pTeacher.Filepath = "null.jpg";
                    }
                    if (string.IsNullOrEmpty(pTeacher.LastName))
                    {
                        pTeacher.LastName = "";
                    }
                    if (pTeacher.FirstName == pTeacher.FatherName)
                    {
                        ViewBag.SuccessMessage = "Student Name and Father's Name not be Same!, Please fill the correct data.";
                        return View("UpdateChangeData");
                    }
                    if (pTeacher.Mobile.Length != 10)
                    {
                        ViewBag.SuccessMessage = "Please Enter the 10 digit Mobile Number.";
                        return View("UpdateChangeData");
                    }
                    string Apiurl = _baseUrl + "api/TeacherAPI/UpdateTeacherDataAPI";

                    string FullAPL = $"{Apiurl}?Id={(string.IsNullOrWhiteSpace(pTeacher.Id.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.Id.ToString()))}" +
                        $"&FirstName={(string.IsNullOrWhiteSpace(pTeacher.FirstName) ? "" : HttpUtility.UrlEncode(pTeacher.FirstName))}" +
                        $"&LastName={(string.IsNullOrWhiteSpace(pTeacher.LastName) ? "" : HttpUtility.UrlEncode(pTeacher.LastName))}" +
                        $"&Subject={(string.IsNullOrWhiteSpace(pTeacher.Subject.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.Subject.ToString()))}" +
                        $"&FatherName={(string.IsNullOrWhiteSpace(pTeacher.FatherName) ? "" : HttpUtility.UrlEncode(pTeacher.FatherName))}" +
                        $"&MotherName={(string.IsNullOrWhiteSpace(pTeacher.MotherName) ? "" : HttpUtility.UrlEncode(pTeacher.MotherName))}" +
                        $"&email={(string.IsNullOrWhiteSpace(pTeacher.Email) ? "" : HttpUtility.UrlEncode(pTeacher.Email))}" +
                        $"&Mobile={(string.IsNullOrWhiteSpace(pTeacher.Mobile) ? "" : HttpUtility.UrlEncode(pTeacher.Mobile))}" +
                        $"&Gender={(string.IsNullOrWhiteSpace(pTeacher.Gender.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.Gender.ToString()))}" +
                        $"&Address={(string.IsNullOrWhiteSpace(pTeacher.Address) ? "" : HttpUtility.UrlEncode(pTeacher.Address))}" +
                        $"&Remarks={(string.IsNullOrWhiteSpace(pTeacher.Remarks) ? "" : HttpUtility.UrlEncode(pTeacher.Remarks))}" +
                        $"&DateOfBirth={(string.IsNullOrWhiteSpace(pTeacher.DateOfBirth.ToString()) ? "" : HttpUtility.UrlEncode(pTeacher.DateOfBirth.ToString()))}" +
                        $"&Filepath={(string.IsNullOrWhiteSpace(pTeacher.Filepath) ? "" : HttpUtility.UrlEncode(pTeacher.Filepath))}";

                    HttpResponseMessage response = await _httpClient.GetAsync(FullAPL);

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
