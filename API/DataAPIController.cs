using CoreProject1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

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
        public IActionResult AddStudentAPI(string FirstName = "", string LastName = "", string FatherName = "", string MotherName = "", string Mobile = "",
            string Gender = "", string Email = "", string Remarks = "", string Class = "", string DateOfBirth = "", string Filepath = "", string Address = "")

        {
            string Message = "";
            bool res = false;

            Student pstudent = new Student();
            int GenderValue = (int)Enum.Parse(typeof(GenderType), Gender);
            int ClassValue = (int)Enum.Parse(typeof(ClassName), Class);
            DateTime DOBValue;
            if (!DateTime.TryParse(DateOfBirth, out DOBValue))
            {
                Message = "Please Select the DateOfBirth in Correct Format";
            }


            pstudent.FirstName = FirstName;
            pstudent.LastName = LastName;
            pstudent.FatherName = FatherName;
            pstudent.MotherName = MotherName;
            pstudent.Mobile = Mobile;
            pstudent.Gender = (GenderType)GenderValue;
            pstudent.Email = Email;
            pstudent.Remarks = Remarks;
            pstudent.Class = (ClassName)ClassValue;
            pstudent.Filepath = Filepath;
            pstudent.Address = Address;
            pstudent.DateOfBirth = DOBValue;

            try
            {
                if (FirstName != "" && FatherName != "" && Email != "")
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("AddStudentMain", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Filepath", string.IsNullOrEmpty(pstudent.Filepath) ? (object)DBNull.Value : pstudent.Filepath);
                            cmd.Parameters.AddWithValue("@FirstName", pstudent.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(pstudent.LastName) ? (object)DBNull.Value : pstudent.LastName);
                            cmd.Parameters.AddWithValue("@Class", pstudent.Class);
                            cmd.Parameters.AddWithValue("@Gender", pstudent.Gender);
                            cmd.Parameters.AddWithValue("@FatherName", pstudent.FatherName);
                            cmd.Parameters.AddWithValue("@MotherName", pstudent.MotherName);
                            cmd.Parameters.AddWithValue("@Address", pstudent.MotherName);
                            cmd.Parameters.AddWithValue("@Remark", pstudent.Remarks);
                            cmd.Parameters.AddWithValue("@Email", pstudent.Email);
                            cmd.Parameters.AddWithValue("@Mobile", pstudent.Mobile);
                            cmd.Parameters.AddWithValue("@DateOfBirth", pstudent.DateOfBirth);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Message = "Data Added Successfully";
                res = true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "An error occurred while processing the request.");
            }

            return Ok(new { message = Message });
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteStudentAPI(int Id)
        {
            bool res = false;
            string message = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteStudentByID", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", Id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                message = "User Deleted Successfully";
                res = true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return Ok(new { Message = message });
        }

        [HttpGet("{Id}")]
        public IActionResult UpdateChangeDataAPI(int Id)
        {
            try
            {
                Student objStudent = new Student();
                using (var con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpdateChangeData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            Id = Convert.ToInt32(rdr["Id"]);
                            objStudent.FirstName = Convert.ToString(rdr["FirstName"]);
                            objStudent.LastName = Convert.ToString(rdr["LastName"]);
                            objStudent.FatherName = Convert.ToString(rdr["FatherName"]);
                            objStudent.MotherName = Convert.ToString(rdr["MotherName"]);
                            objStudent.Gender = (GenderType)Enum.Parse(typeof(GenderType), Convert.ToString(rdr["Gender"]));
                            objStudent.Address = Convert.ToString(rdr["Address"]);
                            objStudent.Class = (ClassName)Enum.Parse(typeof(ClassName), Convert.ToString(rdr["Class"]));
                            objStudent.Remarks = Convert.ToString(rdr["Remark"]);
                            objStudent.Email = Convert.ToString(rdr["Email"]);
                            objStudent.Mobile = Convert.ToString(rdr["Mobile"]);
                            //objStudent.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"]);
                            objStudent.Filepath = Convert.ToString(rdr["Filepath"]);

                        }
                    }

                }
                if (objStudent == null)
                {
                    return RedirectToAction("ViewStudent");
                }
                return Ok(objStudent);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult UpdateStudentDataAPI(int Id, string FirstName = "", string LastName = "", string FatherName = "", string MotherName = "", string Mobile = "",
            string Gender = "", string Email = "", string Remarks = "", string Class = "", string DateOfBirth = "", string Filepath = "", string Address = "")
        {
            string Message = "";
            bool res = false;

            Student std = new Student();
            int GenderValue = (int)Enum.Parse(typeof(GenderType), Gender);
            int ClassValue = (int)Enum.Parse(typeof(ClassName), Class);
            DateTime DOBValue;
            if (!DateTime.TryParse(DateOfBirth, out DOBValue))
            {
                Message = "Please Select the DateOfBirth in Correct Format";
            }

            std.Id = Id;
            std.FirstName = FirstName;
            std.LastName = LastName;
            std.FatherName = FatherName;
            std.MotherName = MotherName;
            std.Mobile = Mobile;
            std.Gender = (GenderType)GenderValue;
            std.Email = Email;
            std.Remarks = Remarks;
            std.Class = (ClassName)ClassValue;
            std.Filepath = Filepath;
            std.Address = Address;
            std.DateOfBirth = DOBValue;
            try
            {

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateStudentData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", std.Id);
                    cmd.Parameters.AddWithValue("@Filepath", std.Filepath);
                    cmd.Parameters.AddWithValue("@FirstName", std.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", std.LastName);
                    cmd.Parameters.AddWithValue("@Class", std.Class);
                    cmd.Parameters.AddWithValue("@Gender", std.Gender);
                    cmd.Parameters.AddWithValue("@FatherName", std.FatherName);
                    cmd.Parameters.AddWithValue("@MotherName", std.MotherName);
                    cmd.Parameters.AddWithValue("@Address", std.Address);
                    cmd.Parameters.AddWithValue("@Remark", std.Remarks);
                    cmd.Parameters.AddWithValue("@Email", std.Email);
                    cmd.Parameters.AddWithValue("@Mobile", std.Mobile);
                    cmd.Parameters.AddWithValue("@DateOfBirth", std.DateOfBirth);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                Message = "Data Updated Successfully";
            }
            catch (Exception ex)
            {
                Message = "Error in updating data: " + ex.Message;
            }

            return Ok(new { message = Message });
        }


        #region "GetStudentData"
        [HttpGet]
        public IActionResult GetStudentData()
        {
            List<Student> ltrStudents = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("StudentCount", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();
                            if (Enum.TryParse<GenderType>(Convert.ToString(rdr["Gender"]), out GenderType gender))
                            {
                                objStudent.Gender = gender;
                            }

                            ltrStudents.Add(objStudent);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
            return Ok(ltrStudents);
        }



        #endregion
    }
}
