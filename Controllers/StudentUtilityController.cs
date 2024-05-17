﻿using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        [Route("View-Student-Record")]
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

                    if (pStudent.FirstName == pStudent.FatherName)
                    {
                        ViewBag.SuccessMessage = "Student Name and Father's Name not be Same! , Please fill the correct data.";
                        return View("AddStudent");
                    }
                    if (pStudent.Mobile.Length != 10)
                    {
                        ViewBag.SuccessMessage = "Please Enter the 10 digit Mobile Number.";
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

                        ViewBag.SuccessMessage = successMessage;
                        return View("AddStudent");
                    }
                    else
                    {
                        ViewBag.SuccessMessage = "Please Enter the Correct Data, Failed!";
                        return View("AddStudent");
                    }
                }
                else
                {
                    ViewBag.SuccessMessage = "Please Enter the Mandatory Field's.";
                    return View("AddStudent");

                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }


        [Route("Add-Student-Record")]
        public IActionResult AddStudent()
        {
            return View();
        }

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
                    string Apiurl = _baseUrl + "api/DataAPI/UpdateStudentDataAPI";


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

        [HttpGet]
        [Route("DeleteView-Student-Record")]
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
