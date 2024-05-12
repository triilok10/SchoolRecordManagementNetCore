using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CoreProject1.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LibraryAPIController : ControllerBase
    {
        private readonly string _connectionString;

        public LibraryAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }
        [HttpGet("{className}")]
        public IActionResult GetClassRecord(string className)
        {
            try
            {
                int classValue = (int)Enum.Parse(typeof(ClassName), className);
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM Student WHERE Class = @ClassName";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", classValue);
                        using (var reader = command.ExecuteReader())
                        {
                            var students = new List<Student>();
                            while (reader.Read())
                            {
                                var student = new Student
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    FirstName = reader["FirstName"].ToString()
                                   
                                };
                                students.Add(student);
                            }
                            return Ok(students);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

    }
}
