using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CoreProject1.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        [HttpPost]
        public IActionResult FeeSubmitPost([FromBody] Student student)
        {

            return Ok();
        }
    }
}
