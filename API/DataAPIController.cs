using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CoreProject1.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DataAPIController : ControllerBase
    {
        private readonly string _connectionString;

        public DataAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }

        [HttpGet]
        public IActionResult ViewStudentAPI()
        {
            List<Student> ltrStudents = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddViewStudents", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();

                            objStudent.Id = Convert.ToInt32(rdr["ID"]);
                            objStudent.FirstName = Convert.ToString(rdr["FirstName"]);
                            objStudent.LastName = Convert.ToString(rdr["LastName"]);
                            objStudent.FatherName = Convert.ToString(rdr["FatherName"]);
                            objStudent.MotherName = Convert.ToString(rdr["MotherName"]);
                            objStudent.Address = Convert.ToString(rdr["Address"]);
                            objStudent.Remarks = Convert.ToString(rdr["Remark"]);
                            objStudent.Mobile = Convert.ToString(rdr["Mobile"]);
                            objStudent.Filepath = Convert.ToString(rdr["Filepath"]);

                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                            }
                            if (Enum.TryParse<ClassName>(Convert.ToString(rdr["Class"]), out ClassName classname))
                            {
                                objStudent.Class = classname;
                            }
                            ltrStudents.Add(objStudent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, "An error occurred while processing your request.");
            }
            return Ok(ltrStudents);
        }

        [HttpGet]
        public IActionResult AddStudentAPI(Student pStudent)
        {
            try
            {
                
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddStudentMain", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Filepath", pStudent.Filepath);
                        cmd.Parameters.AddWithValue("@FirstName", pStudent.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(pStudent.LastName) ? (object)DBNull.Value : pStudent.LastName);
                        cmd.Parameters.AddWithValue("@Class", pStudent.Class);
                        cmd.Parameters.AddWithValue("@Gender", pStudent.Gender);
                        cmd.Parameters.AddWithValue("@FatherName", pStudent.FatherName);
                        cmd.Parameters.AddWithValue("@MotherName", pStudent.MotherName);
                        cmd.Parameters.AddWithValue("@Address", pStudent.Address);
                        cmd.Parameters.AddWithValue("@Remark", pStudent.Remarks);
                        cmd.Parameters.AddWithValue("@Email", pStudent.Email);
                        cmd.Parameters.AddWithValue("@Mobile", pStudent.Mobile);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "An error occurred while processing the request.");
            }

            return Ok(new { Message = "User Record " + pStudent.FirstName + " successfully saved!" });
        }


    }
}
