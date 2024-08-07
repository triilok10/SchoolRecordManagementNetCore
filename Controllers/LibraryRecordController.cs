﻿using Azure;
using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Security.Claims;
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

        #region "ViewBooks"
        [HttpGet]
        [Route("View-Books")]
        public async Task<IActionResult> ViewBooks()
        {

            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
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
            else
            {

                TempData["SuccessMessage"] = "Please login to View Books";
                return RedirectToAction("LogIn", "Log");

            }

        }

        #endregion

        #region "Add Books"
        [Route("Add-Books")]
        public IActionResult AddBooks()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    return View(new Student());
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Please login to Add Books";
                return RedirectToAction("LogIn", "Log");
            }


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
                    TempData["SuccessMessage"] = successMessage;
                    return RedirectToAction("AddBooks");
                }

            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return RedirectToAction("AddBooks");
        }


        #endregion

        #region "Update Books"
        [Route("Update-Books")]
        public async Task<IActionResult> UpdateBooks()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
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
            else
            {
                TempData["SuccessMessage"] = "Please login to UpdateBooks";
                return RedirectToAction("LogIn", "Log");
            }
        }


        [HttpGet]
        public async Task<IActionResult> PostUpdate(int Id)
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    string APIURL = $"{_baseUrl}api/LibraryAPI/updateBookAPI/{Id}";
                    HttpResponseMessage response = await _httpClient.GetAsync(APIURL);


                    if (response.IsSuccessStatusCode)
                    {
                        string ResponseBody = await response.Content.ReadAsStringAsync();
                        Student LibUpdate = JsonConvert.DeserializeObject<Student>(ResponseBody);
                        return View(LibUpdate);
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
                TempData["SuccessMessage"] = "Please login to PostUpdate";
                return RedirectToAction("LogIn", "Log");
            }


        }


        [HttpPost]
        public async Task<IActionResult> UpdateLibBooksPost(Student objBooks)
        {
            bool res = false;
            string successMessage = "";
            try
            {
                if (string.IsNullOrWhiteSpace(objBooks.Id.ToString()) && string.IsNullOrWhiteSpace(objBooks.BookName) && string.IsNullOrWhiteSpace(objBooks.BookAuthorName) && string.IsNullOrWhiteSpace(objBooks.BookMediumLanguage.ToString()))
                {
                    res = false;
                    successMessage = "Please Pass all the Paremeters";
                }

                string apiurl = $"{_baseUrl}api/LibraryAPI/PostUpdateBookAPI";

                string FullUrl = $"{apiurl}?BookName={(string.IsNullOrWhiteSpace(objBooks.BookName) ? "" : HttpUtility.UrlEncode(objBooks.BookName))}" +
                    $"&BookAuthorName={(string.IsNullOrWhiteSpace(objBooks.BookAuthorName) ? "" : HttpUtility.UrlEncode(objBooks.BookAuthorName))}" +
                    $"&BookMedium={(string.IsNullOrWhiteSpace(objBooks.BookMediumLanguage.ToString()) ? "" : HttpUtility.UrlEncode(objBooks.BookMediumLanguage.ToString()))}" +
                    $"&BookId={(string.IsNullOrWhiteSpace(objBooks.Id.ToString()) ? "" : HttpUtility.UrlEncode(objBooks.Id.ToString()))}";

                HttpResponseMessage Response = await _httpClient.GetAsync(FullUrl);
                if (Response.IsSuccessStatusCode)
                {
                    string ResponseBody = await Response.Content.ReadAsStringAsync();
                    var responseObject = JsonConvert.DeserializeObject<dynamic>(ResponseBody);
                    successMessage = responseObject.msg;
                    TempData["SuccessMessage"] = successMessage;
                    return RedirectToAction("PostUpdate");
                }
                else
                {
                    TempData["SuccessMessage"] = "Book Updation Failed!, Please Check Again";
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View();
        }


        #endregion

        #region "Delete Books"
        [Route("Get-Book-Data-by-Id")]
        public IActionResult GetBookDatabyId(int Id)
        {
            return View();
        }

        [Route("Delete-Books")]
        public async Task<IActionResult> DeleteBooks()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
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
            else
            {
                TempData["SuccessMessage"] = "Please login to DeleteBooks";
                return RedirectToAction("LogIn", "Log");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteBookPost(int Id)
        {
            bool res = false;
            string successMessage = "";
            try
            {
                string apiurl = $"{_baseUrl}api/LibraryAPI/DeleteBookPostAPI/{Id}";
                HttpResponseMessage response = await _httpClient.DeleteAsync(apiurl);
                if (response.IsSuccessStatusCode)
                {
                    string ResponseBody = await response.Content.ReadAsStringAsync();
                    var ResponseMessage = JsonConvert.DeserializeObject<dynamic>(ResponseBody);
                    successMessage = ResponseMessage.msg;
                    ViewBag.SuccessMessage = successMessage;
                    return View("DeleteBooks");
                }
                else
                {
                    ViewBag.SuccessMessage = "Book Deletion Failed!, Please Check Again";
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
            return View();
        }
        #endregion

        #region "Book Issue"
        [Route("Issue-Book")]
        public IActionResult BookIssue()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Please login to BookIssue";
                return RedirectToAction("LogIn", "Log");
            }


        }


        [HttpPost]
        [Route("Book-Issue")]
        public async Task<IActionResult> BookIssueStd(int StudentId, string StudentFirstName, string StudentLastName, string StudentClass)
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    int HdnStudentId = StudentId;
                    string HdnFirstName = StudentFirstName;
                    string HdnLastName = StudentLastName;
                    int hdnClass = (int)Enum.Parse(typeof(ClassName), StudentClass);


                    HttpResponseMessage response = await _httpClient.GetAsync(_baseUrl + "api/LibraryAPI/ViewBooksAPI");

                    if (response.IsSuccessStatusCode)
                    {
                        string responsebody = await response.Content.ReadAsStringAsync();
                        List<Student> objBooks = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                        foreach (var student in objBooks)
                        {
                            student.HdnStudentId = HdnStudentId;
                            student.FirstName = HdnFirstName;
                            student.LastName = HdnLastName;
                            student.Class = (ClassName)hdnClass;
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
            else
            {
                TempData["SuccessMessage"] = "Please login to BookIssueStd";
                return RedirectToAction("LogIn", "Log");
            }
        }

        #endregion

        #region "GetBookByClass"

        [HttpPost]
        [Route("Class-Student-Lib")]
        public async Task<IActionResult> GetBookByClassLib(string Class)
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

        #endregion

        #region "GetBookIssuedStudent"
        [HttpPost]
        public async Task<IActionResult> GetBookIssuedStudent(string Class)
        {

            if (!string.IsNullOrEmpty(Class))
            {
                string APIURL = $"{_baseUrl}api/LibraryAPI/GetBookIssuedStudentAPI/{Class}";
                HttpResponseMessage Response = await _httpClient.GetAsync(APIURL);
                if (Response.IsSuccessStatusCode)
                {
                    string ResponseBody = await Response.Content.ReadAsStringAsync();
                    List<Student> students = JsonConvert.DeserializeObject<List<Student>>(ResponseBody);
                    return View("SubmitBook", students);
                }

            }

            return View();
        }

        #endregion

        #region "SelectBookByStd"

        [HttpPost]
        public async Task<IActionResult> SelectBookByStd(int BookId, int HdnStudentId, string StudentFirstName, string StudentLastName, string StudentClass, string IssueDateTime, string hdnBookAuthor, string hdnBookName)
        {
            string successMessage = "";
            try
            {
                if (string.IsNullOrWhiteSpace(StudentFirstName) && string.IsNullOrWhiteSpace(StudentLastName))
                {
                    successMessage = "Student Name and FatherName must not be null!";
                    return View("");
                }
                if (HdnStudentId != null && BookId != null)
                {
                    string FullName = StudentFirstName + " " + StudentLastName;

                    string apiurl = $"{_baseUrl}api/LibraryAPI/IssueBooktoStd";
                    string fullurl = $"{apiurl}?BookId={(string.IsNullOrWhiteSpace(BookId.ToString()) ? "" : HttpUtility.UrlEncode(BookId.ToString()))}" +
                    $"&HdnStudentId={(string.IsNullOrWhiteSpace(HdnStudentId.ToString()) ? "" : HttpUtility.UrlEncode(HdnStudentId.ToString()))}" +
                    $"&FullName={(string.IsNullOrWhiteSpace(FullName) ? "" : HttpUtility.UrlEncode(FullName))}" +
                    $"&StudentClass={(string.IsNullOrWhiteSpace(StudentClass) ? "" : HttpUtility.UrlEncode(StudentClass))}" +
                    $"&IssueDateTime={(string.IsNullOrWhiteSpace(IssueDateTime) ? "" : HttpUtility.UrlEncode(IssueDateTime))}" +
                    $"&hdnBookAuthor={(string.IsNullOrWhiteSpace(hdnBookAuthor) ? "" : HttpUtility.UrlEncode(hdnBookAuthor))}" +
                    $"&hdnBookName={(string.IsNullOrWhiteSpace(hdnBookName) ? "" : HttpUtility.UrlEncode(hdnBookName))}";
                    ;
                    HttpResponseMessage response = await _httpClient.GetAsync(fullurl);
                    if (response.IsSuccessStatusCode)
                    {
                        string ResponseBody = await response.Content.ReadAsStringAsync();
                        var ResponseObject = JsonConvert.DeserializeObject<dynamic>(ResponseBody);
                        successMessage = ResponseObject.message;

                        ViewBag.SuccessMessage = successMessage;
                        return View("BookIssueStd");
                    }
                    else
                    {
                        ViewBag.SuccessMessage = "Book Issued Failed!, Please Check Again";
                    }
                }

            }
            catch (Exception ex)
            {
               
            }
            return Ok();
        }

        #endregion

        #region "IssuedBooks"

        [HttpGet]
        [Route("IssuedBook")]
        public async Task<IActionResult> IssuedBook()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    HttpResponseMessage Response = await _httpClient.GetAsync(_baseUrl + "api/LibraryAPI/CheckIssuedBookAPI");
                    if (Response.IsSuccessStatusCode)
                    {
                        string responsebody = await Response.Content.ReadAsStringAsync();
                        List<Student> IssuedBook = JsonConvert.DeserializeObject<List<Student>>(responsebody);
                        return View(IssuedBook);
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
                TempData["SuccessMessage"] = "Please login to IssuedBook";
                return RedirectToAction("LogIn", "Log");
            }

        }

        #endregion

        #region "SubmitBook"

        [HttpGet]
        [Route("SubmitBook")]
        public IActionResult SubmitBook()
        {
            var IsLogginIn = HttpContext.Session.GetString("IsLoggedIn");
            var LoginUsername = HttpContext.Session.GetString("Username");
            var LoginPassword = HttpContext.Session.GetString("Password");
            if (IsLogginIn == "true" && LoginUsername != null && LoginPassword != null)
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Please login to SubmitBook";
                return RedirectToAction("LogIn", "Log");
            }
        }


        [HttpPost]
        public async Task<IActionResult> SubmitIssueBook(int HdnBookId)
        {
            string successMessage = "";
            string APIURL = $"{_baseUrl}api/LibraryAPI/SubmitBookAPI/{HdnBookId}";
            HttpResponseMessage Response = await _httpClient.PostAsync(APIURL, null);

            if (Response.IsSuccessStatusCode)
            {
                string ResponseBody = await Response.Content.ReadAsStringAsync();
                dynamic ResponseObject = JsonConvert.DeserializeObject<dynamic>(ResponseBody);
                successMessage = ResponseObject.message;

                ViewBag.SuccessMessage = successMessage;
                return View("SubmitBook");
            }

            return Ok();
        }

        #endregion
    }
}
