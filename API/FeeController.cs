using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CoreProject1.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeeController : ControllerBase
    {
        private readonly string _connectionString;

        public FeeController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }


        [HttpPost]
        public IActionResult FeeSubmitPost([FromBody] Student student)
        {
            string Message = "";
            int classValue = (int)student.Class;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_FeeSubmitSet", con);
                    con.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", student.Id);
                    cmd.Parameters.AddWithValue("@StudentClass", classValue);
                    cmd.Parameters.AddWithValue("@StudentFee", student.FeeAmount);
                    cmd.Parameters.AddWithValue("@StudentName", student.FirstName);
                    cmd.Parameters.AddWithValue("@StudentFatherName", student.FatherName);
                    cmd.ExecuteNonQuery();
                }
                Message = "Fee Paid Successfully";
            }
            catch (Exception ex)
            {
                return Ok(new { msg = ex.Message });
            }
            return Ok(new { msg = Message });
        }
    }
}
