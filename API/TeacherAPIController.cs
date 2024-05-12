using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CoreProject1.API
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeacherAPIController : ControllerBase
    {

        private readonly string _connectionString;

        public TeacherAPIController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("CustomConnection");
        }

        #region "View Teacher API"
        [HttpGet]
        public IActionResult ViewTeacherAPI()
        {
            List<Student> ltrStudents = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("AddViewTeachers", con);
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
                            objStudent.FatherName = Convert.ToString(rdr["FathersName"]);
                            objStudent.MotherName = Convert.ToString(rdr["MotherName"]);
                            objStudent.Address = Convert.ToString(rdr["Address"]);
                            objStudent.Remarks = Convert.ToString(rdr["Remarks"]);
                            objStudent.Email = Convert.ToString(rdr["Email"]);
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

        #endregion

        #region "Add Teacher API"
        [HttpPost]
        public IActionResult AddTeacherAPI(TeacherDetail pStudent)
        {
            string Message = "";
            bool res = false;
            try
            {
                if (pStudent.FirstName != "" && pStudent.FatherName != "" && pStudent.Email != "")
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("AddTeacherMain", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Filepath", string.IsNullOrEmpty(pStudent.Filepath) ? (object)DBNull.Value : pStudent.Filepath);
                            cmd.Parameters.AddWithValue("@FirstName", pStudent.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(pStudent.LastName) ? (object)DBNull.Value : pStudent.LastName);
                            cmd.Parameters.AddWithValue("@Class", pStudent.Class);
                            cmd.Parameters.AddWithValue("@Gender", pStudent.Gender);
                            cmd.Parameters.AddWithValue("@FathersName", pStudent.FatherName);
                            cmd.Parameters.AddWithValue("@MotherName", pStudent.MotherName);
                            cmd.Parameters.AddWithValue("@Address", pStudent.Address);
                            cmd.Parameters.AddWithValue("@Remark", pStudent.Remarks);
                            cmd.Parameters.AddWithValue("@Email", pStudent.Email);
                            cmd.Parameters.AddWithValue("@Mobile", pStudent.Mobile);
                            cmd.Parameters.AddWithValue("@DateOfBirth", pStudent.DateOfBirth);


                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                Message = "Teacher Data Added Successfully";
                res = true;
            }
            catch (Exception ex)
            {
                // Handle exception
                return StatusCode(500, "There is a issue in the Database Coloumns, Please Check!");
            }

            return Ok(new { message = Message });
        }

        #endregion

        #region "Delete Teacher API"

        [HttpDelete("{Id}")]
        public IActionResult DeleteTeacherAPI(int Id)
        {
            bool res = false;
            string message = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteTeacherByID", con);
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
            }
            return Ok(new { Message = message });
        }

        #endregion

        #region "Update Teacher API"

        [HttpGet("{Id}")]
        public IActionResult UpdateChangeDataAPI(int Id)
        {
            try
            {
                TeacherDetail objStudent = null;
                using (var con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("UpChangeData", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            objStudent = new TeacherDetail
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                FirstName = Convert.ToString(rdr["FirstName"]),
                                LastName = Convert.ToString(rdr["LastName"]),
                                FatherName = Convert.ToString(rdr["FathersName"]),
                                MotherName = Convert.ToString(rdr["MotherName"]),
                                Gender = (GenderTypes)Enum.Parse(typeof(GenderTypes), Convert.ToString(rdr["Gender"])),
                                Address = Convert.ToString(rdr["Address"]),
                                Class = (Degination)Enum.Parse(typeof(Degination), Convert.ToString(rdr["Class"])),
                                Remarks = Convert.ToString(rdr["Remarks"]),
                                Email = Convert.ToString(rdr["Email"]),
                                Mobile = Convert.ToString(rdr["Mobile"]),
                                DateOfBirth = Convert.ToString(rdr["DateOfBirth"])
                            };
                        }
                    }

                }
                if (objStudent == null)
                {
                    return RedirectToAction("ViewTeacher");
                }
                return Ok(objStudent);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult UpdateTeacherDataAPI(TeacherDetail std)
        {
            string Message = "";
            try
            {

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateTeacherData", con);
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

        #endregion
    }
}
