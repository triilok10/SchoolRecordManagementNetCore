using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoreProject1.Controllers
{
    public class StudentUtilityController : Controller
    {
       

        //[HttpGet]
        //public async Task<IActionResult> ViewStudent()
        //{
        //    try
        //    {
        //        HttpResponseMessage response = await _httpClient.GetAsync(baseurl + "api/DataAPIController/ViewStudentAPI");

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
        //        // Log the exception or handle it appropriately
        //        return View("Error");
        //    }
        //}


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
