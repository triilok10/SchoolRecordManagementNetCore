using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

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

        [HttpGet]
        public async Task<IActionResult> ViewStudent()
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

        //[HttpPost]
        //public async Task<IActionResult> CreateStudentData(Student pStudent, IFormFile File)
        //{
        //    try
        //    {
        //        if (pStudent.FirstName != "" && pStudent.FatherName != "" && pStudent.Email != "")
        //        {
        //            if (File != null && File.Length > 0)
        //            {
        //                string FileName = Path.GetFileName(File.FileName);
        //                string FilePathData = Path.Combine("wwwroot", "Images", "UserImages", FileName);
        //                using (var stream = new FileStream(FilePathData, FileMode.Create))
        //                {
        //                    File.CopyTo(stream);
        //                }

        //                pStudent.Filepath = FileName;
        //            }
        //        }

        //        string Apiurl = _baseUrl + "api/DataAPI/AddStudentAPI";

        //        string fullurl = $"{Apiurl}?FirstName={(string.IsNullOrWhiteSpace(pStudent.FirstName) ? "" : HttpUtility.UrlEncode(pStudent.FirstName))}" +
        //            $"&LastName={(string.IsNullOrWhiteSpace(pStudent.LastName) ? "" : HttpUtility.UrlEncode(pStudent.LastName))}" +
        //            $"&FatherName={(string.IsNullOrWhiteSpace(pStudent.FatherName) ? "" : HttpUtility.UrlEncode(pStudent.FatherName))}" +
        //            $"&MotherName={(string.IsNullOrWhiteSpace(pStudent.MotherName) ? "" : HttpUtility.UrlEncode(pStudent.MotherName))}" +
        //            $"&Mobile={(string.IsNullOrWhiteSpace(pStudent.Mobile) ? "" : HttpUtility.UrlEncode(pStudent.Mobile))}" +
        //            $"&Class={(string.IsNullOrWhiteSpace(pStudent.Class.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Class.ToString()))}" +
        //            $"&Gender={(string.IsNullOrWhiteSpace(pStudent.Gender.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Gender.ToString()))}" +
        //            $"&Address={(string.IsNullOrWhiteSpace(pStudent.Address) ? "" : HttpUtility.UrlEncode(pStudent.Address))}" +
        //            $"&Remarks={(string.IsNullOrWhiteSpace(pStudent.Remarks) ? "" : HttpUtility.UrlEncode(pStudent.Remarks))}" +
        //            $"&Filepath={(string.IsNullOrWhiteSpace(pStudent.Filepath) ? "" : HttpUtility.UrlEncode(pStudent.Filepath))}" +
        //            $"&Email={(string.IsNullOrWhiteSpace(pStudent.Email) ? "" : HttpUtility.UrlEncode(pStudent.Email))}";


        //        HttpResponseMessage response = await _httpClient.GetAsync(fullurl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responsebody = await response.Content.ReadAsStringAsync();
        //            List<Student> objstudent = JsonConvert.DeserializeObject<List<Student>>(responsebody);
        //            return View(objstudent);
        //        }
        //        else
        //        {

        //            return View("Error");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return View("Error");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> CreateStudentData(Student pStudent, IFormFile File)
        {
            try
            {
                if (pStudent.FirstName != "" && pStudent.FatherName != "" && pStudent.Email != "")
                {
                    // Process file upload if present
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

                    string Apiurl = _baseUrl + "api/DataAPI/AddStudentAPI";


                    string jsonStudent = JsonConvert.SerializeObject(pStudent);


                    var content = new StringContent(jsonStudent, Encoding.UTF8, "application/json");


                    HttpResponseMessage response = await _httpClient.PostAsync(Apiurl, content);


                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                        string successMessage = responseObject.message;


                        ViewBag.SuccessMessage = successMessage;
                        return View("AddStudent");
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



        public IActionResult AddStudent()
        {
            return View();
        }

        public IActionResult UpdateStudent()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DeleteStudent()
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

                    TempData["SuccessMessage"] = message;

                    return RedirectToAction("Students", "Home");

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

    }
}
