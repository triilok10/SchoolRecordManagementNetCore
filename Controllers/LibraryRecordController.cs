﻿using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace CoreProject1.Controllers
{
    public class LibraryRecordController : Controller
    {
        private readonly HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;
        private readonly dynamic _baseUrl;

        public LibraryRecordController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host.Value}/"; _httpClient.BaseAddress = new Uri(_baseUrl);

        }

        [HttpGet]
        [Route("View-Books")]
        public async Task<IActionResult> ViewBooks()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/LibraryAPI/ViewBooksAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objBooks = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    return View(objBooks);
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

        [Route("Add-Books")]
        public IActionResult AddBooks()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBookInfo(Student objBooks)
        {
            string successMessage = "";
            try
            {
                string ApiUrl = $"{_baseUrl}api/LibraryAPI/AddNewBooks";
                string Fullurl = $"{ApiUrl}?BookName={(string.IsNullOrWhiteSpace(objBooks.BookName) ? "" : HttpUtility.UrlEncode(objBooks.BookName))}" +
                    $"&BookAuthorName={(string.IsNullOrWhiteSpace(objBooks.BookAuthorName) ? "" : HttpUtility.UrlEncode(objBooks.BookAuthorName))}" +
                    $"&BookMediumLanguage={(string.IsNullOrWhiteSpace(objBooks.BookMediumLanguage.ToString()) ? "" : HttpUtility.UrlEncode(objBooks.BookMediumLanguage.ToString()))}";


                HttpResponseMessage response = await _httpClient.GetAsync(Fullurl);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    successMessage = responseObject.message;

                    ViewBag.SuccessMessage = successMessage;
                    return View("AddBooks");
                }

            }
            catch (Exception ex)
            {

            }

            return View();
        }

        [Route("Update-Books")]
        public async Task<IActionResult> UpdateBooks()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/LibraryAPI/ViewBooksAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objBooks = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    return View(objBooks);
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
        [Route("Get-Book-Data-by-Id")]
        public IActionResult GetBookDatabyId(int Id)
        {
            return View();
        }

        [Route("Delete-Books")]
        public IActionResult DeleteBooks()
        {
            return View();
        }

        [Route("Issue-Book")]
        public IActionResult BookIssue()
        {
            return View();
        }


        [HttpPost]
        [Route("Book-Issue")]
        public async Task<IActionResult> BookIssueStd(int StudentId)
        {
            try
            {
                int HdnStudentId = StudentId;
                HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/LibraryAPI/ViewBooksAPI");

                if (response.IsSuccessStatusCode)
                {
                    string responsebody = await response.Content.ReadAsStringAsync();
                    List<Student> objBooks = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                    foreach (var student in objBooks)
                    {
                        student.HdnStudentId = HdnStudentId;
                    }
                    return View(objBooks);
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
        [Route("Class-Student")]
        public async Task<IActionResult> GetBookByClass(string Class)
        {
            try
            {
                if (!string.IsNullOrEmpty(Class))
                {
                    string apiUrl = $"{_baseUrl}api/LibraryAPI/GetClassRecord/{Class}";
                    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();

                        List<Student> students = JsonConvert.DeserializeObject<List<Student>>(responseBody);

                        return View("BookIssue", students);
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

        [HttpPost]
        public IActionResult SelectBookByStd(int BookId, int HdnStudentId)
        {
            try
            {
                string apiurl = $"{_baseUrl}api/LibraryAPI/GetClassRecord/";
                string fullurl = $"{apiurl}?BookId={(string.IsNullOrWhiteSpace(BookId.ToString()) ? "" : HttpUtility.UrlEncode(BookId.ToString()))}" +
                $"&HdnStudentId={(string.IsNullOrWhiteSpace(HdnStudentId.ToString()) ? "" : HttpUtility.UrlEncode(HdnStudentId.ToString()))}";
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }

    }
}
