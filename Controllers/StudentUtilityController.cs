﻿using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace CoreProject1.Controllers
{
    public class StudentUtilityController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public StudentUtilityController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = "https://localhost:7054/";

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

        [HttpPost]
        public async Task<IActionResult> CreateStudentData(Student pStudent, IFormFile File)
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
                            File.CopyTo(stream);
                        }

                        pStudent.Filepath = FileName;
                    }
                }

                string Apiurl = _baseUrl + "api/DataAPI/AddStudentAPI";

                string fullurl = $"{Apiurl}?FirstName={(string.IsNullOrWhiteSpace(pStudent.FirstName) ? "" : HttpUtility.UrlEncode(pStudent.FirstName))}" +
                    $"&LastName={(string.IsNullOrWhiteSpace(pStudent.LastName) ? "" : HttpUtility.UrlEncode(pStudent.LastName))}" +
                    $"&FatherName={(string.IsNullOrWhiteSpace(pStudent.FatherName) ? "" : HttpUtility.UrlEncode(pStudent.FatherName))}" +
                    $"&MotherName={(string.IsNullOrWhiteSpace(pStudent.MotherName) ? "" : HttpUtility.UrlEncode(pStudent.MotherName))}" +
                    $"&Mobile={(string.IsNullOrWhiteSpace(pStudent.Mobile) ? "" : HttpUtility.UrlEncode(pStudent.Mobile))}" +
                    $"&Class={(string.IsNullOrWhiteSpace(pStudent.Class.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Class.ToString()))}" +
                    $"&Gender={(string.IsNullOrWhiteSpace(pStudent.Gender.ToString()) ? "" : HttpUtility.UrlEncode(pStudent.Gender.ToString()))}" +
                    $"&Address={(string.IsNullOrWhiteSpace(pStudent.Address) ? "" : HttpUtility.UrlEncode(pStudent.Address))}" +
                    $"&Remarks={(string.IsNullOrWhiteSpace(pStudent.Remarks) ? "" : HttpUtility.UrlEncode(pStudent.Remarks))}" +
                    $"&Filepath={(string.IsNullOrWhiteSpace(pStudent.Filepath) ? "" : HttpUtility.UrlEncode(pStudent.Filepath))}" +
                    $"&Email={(string.IsNullOrWhiteSpace(pStudent.Email) ? "" : HttpUtility.UrlEncode(pStudent.Email))}";


                HttpResponseMessage response = await _httpClient.GetAsync(fullurl);

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


        public IActionResult AddStudent()
        {
            return View();
        }

        public IActionResult UpdateStudent()
        {
            return View();
        }

        public IActionResult DeleteStudent()
        {
            return View();
        }
    }
}
