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
        public IActionResult AddTeacherAPI(string FirstName = "", string LastName = "", string FatherName = "", string MotherName = "", string Mobile = "",
            string Gender = "", string Email = "", string Remarks = "", string Subject = "", string DateOfBirth = "", string Filepath = "", string Address = "")
        {
            TeacherDetail pTeacher = new TeacherDetail();
            string Message = "";
            bool res = false;
            int GenderValue = (int)Enum.Parse(typeof(GenderType), Gender);
            int ClassValue = (int)Enum.Parse(typeof(Degination), Subject);
            DateTime DOBValue;
            if (!DateTime.TryParse(DateOfBirth, out DOBValue))
            {
                Message = "Please Select the DateOfBirth in Correct Format";
            }

            pTeacher.FirstName = FirstName;
            pTeacher.LastName = LastName;
            pTeacher.FatherName = FatherName;
            pTeacher.MotherName = MotherName;
            pTeacher.Mobile = Mobile;
            pTeacher.Gender = (GenderTypes)GenderValue;
            pTeacher.Email = Email;
            pTeacher.Remarks = Remarks;
            pTeacher.Subject = (Degination)ClassValue;
            pTeacher.Filepath = Filepath;
            pTeacher.Address = Address;
            pTeacher.DateOfBirth = DOBValue;

            try
            {
                if (pTeacher.FirstName != "" && pTeacher.FatherName != "" && pTeacher.Email != "")
                {
                    using (SqlConnection con = new SqlConnection(_connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("AddTeacherMain", con))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Filepath", string.IsNullOrEmpty(pTeacher.Filepath) ? (object)DBNull.Value : pTeacher.Filepath);
                            cmd.Parameters.AddWithValue("@FirstName", pTeacher.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", string.IsNullOrEmpty(pTeacher.LastName) ? (object)DBNull.Value : pTeacher.LastName);
                            cmd.Parameters.AddWithValue("@Class", pTeacher.Subject);
                            cmd.Parameters.AddWithValue("@Gender", pTeacher.Gender);
                            cmd.Parameters.AddWithValue("@FathersName", pTeacher.FatherName);
                            cmd.Parameters.AddWithValue("@MotherName", pTeacher.MotherName);
                            cmd.Parameters.AddWithValue("@Address", pTeacher.Address);
                            cmd.Parameters.AddWithValue("@Remark", pTeacher.Remarks);
                            cmd.Parameters.AddWithValue("@Email", pTeacher.Email);
                            cmd.Parameters.AddWithValue("@Mobile", pTeacher.Mobile);
                            cmd.Parameters.AddWithValue("@DateOfBirth", pTeacher.DateOfBirth);


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
                                Subject = (Degination)Enum.Parse(typeof(Degination), Convert.ToString(rdr["Class"])),
                                Remarks = Convert.ToString(rdr["Remarks"]),
                                Email = Convert.ToString(rdr["Email"]),
                                Mobile = Convert.ToString(rdr["Mobile"]),
                                DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"])
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
                    cmd.Parameters.AddWithValue("@Class", std.Subject);
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
