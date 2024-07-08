using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace CoreProject1.Controllers
{
    public class FeeReportDataController : Controller
    {
        private readonly HttpClient _httpClient;
        IHttpContextAccessor _httpContextAccessor;
        private readonly dynamic _baseUrl;

        public FeeReportDataController(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            var request = _httpContextAccessor.HttpContext.Request;
            _baseUrl = $"{request.Scheme}://{request.Host.Value}/"; _httpClient.BaseAddress = new Uri(_baseUrl);

        }

        #region "GetBookByClass"

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

                        return View("FeeReport", students);
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

        [HttpGet]
        public async Task<IActionResult> FeeReportGet(int Id)
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
        public async Task<IActionResult> FeeSubmitPost(Student student)
        {

            try
            {
                string apiurl = $"{_baseUrl}api/Fee/FeeSubmitPost";
                string jsonContent = JsonConvert.SerializeObject(student);
                StringContent content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await _httpClient.PostAsync(apiurl, content);
                if (res.IsSuccessStatusCode)
                {
                    string resbody = await res.Content.ReadAsStringAsync();
                    dynamic resdata = JsonConvert.DeserializeObject<dynamic>(resbody);
                    string Message = resdata.msg;
                    ModelState.Clear();
                    TempData["message"] = Message;
                    TempData.Keep("message");
                    return RedirectToAction("FeeReport", "Home");

                }
                else
                {
                    return View("error");
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }
    }
}
