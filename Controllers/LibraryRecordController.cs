using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [Route("View-Books")]
        public IActionResult ViewBooks()
        {
            return View();
        }

        [Route("Add-Books")]
        public IActionResult AddBooks()
        {
            return View();
        }

        [Route("Update-Books")]
        public IActionResult UpdateBooks()
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

                        TempData["Students"] = students;
                        return RedirectToAction("BookIssue");
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

    }
}
