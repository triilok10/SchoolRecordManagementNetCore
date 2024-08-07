﻿using CoreProject1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.Pkcs;

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
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    FatherName = reader["Fathername"].ToString(),
                                    MotherName = reader["Mothername"].ToString(),
                                    Class = (ClassName)Convert.ToInt32(reader["Class"]),
                                    Gender = (GenderType)Convert.ToInt32(reader["Gender"]),
                                    Email = reader["Email"].ToString(),
                                    Mobile = reader["MOBILE"].ToString(),
                                    FeeAmount = Convert.ToInt32(reader["FeeReport"]),
                                    IsFeePaid = Convert.ToBoolean(reader["IsFeePaid"])
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

        [HttpGet("{className}")]
        public IActionResult GetBookIssuedStudentAPI(string className)
        {
            try
            {
                //int classValue = (int)Enum.Parse(typeof(ClassName), className);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = $" Select * from Library Where LEN(RTRIM(BOOK1IssueId))>0 and  Book1IssueClass= @ClassName";
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ClassName", className);
                        using (var reader = command.ExecuteReader())
                        {
                            var students = new List<Student>();
                            while (reader.Read())
                            {
                                var student = new Student
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    StudentName = reader["Book1IssuedTo"].ToString(),
                                    BookName = reader["Book1"].ToString(),
                                    BookAuthorName = reader["Book1Publisher"].ToString(),
                                    Class = (ClassName)Convert.ToInt32(reader["Book1IssueClass"]),
                                    HdnStudentId = Convert.ToInt32(reader["BOOK1IssueId"])

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


        [HttpGet]
        public IActionResult AddNewBooks(string BookName, string BookAuthorName, string BookMediumLanguage)
        {
            string Message = "";
            bool res = false;
            int BookMediumLang = (int)Enum.Parse(typeof(BookMedium), BookMediumLanguage);
            try
            {
                Student objBooks = new Student();
                objBooks.BookName = BookName;
                objBooks.BookAuthorName = BookAuthorName;
                objBooks.BookMediumLanguage = (BookMedium)BookMediumLang;

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("AddBooksInLib", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Book1", objBooks.BookName);
                        cmd.Parameters.AddWithValue("@Book1Publisher", objBooks.BookAuthorName);
                        cmd.Parameters.AddWithValue("Book1Medium", objBooks.BookMediumLanguage);
                        cmd.ExecuteNonQuery();
                    }
                    Message = "Book Added Successfully in the Library Record";
                    res = true;
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(new { message = Message });
        }


        [HttpGet]
        public IActionResult ViewBooksAPI()
        {
            List<Student> objBooks = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ViewBooks", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();

                            objStudent.Id = Convert.ToInt32(rdr["ID"]);
                            objStudent.BookName = Convert.ToString(rdr["Book1"]);
                            objStudent.BookAuthorName = Convert.ToString(rdr["Book1Publisher"]);
                            if (Enum.TryParse<BookMedium>(Convert.ToString(rdr["Book1Medium"]), out BookMedium BookMediumLanguage))
                            {
                                objStudent.BookMediumLanguage = BookMediumLanguage;
                            }
                            objBooks.Add(objStudent);
                        }
                    }

                }

            }
            catch (Exception ex)
            {

            }
            return Ok(objBooks);
        }

        [HttpGet]
        public IActionResult IssueBooktoStd(int BookId, int HdnStudentId, string FullName, string StudentClass, string IssueDateTime, string hdnBookAuthor, string hdnBookName)
        {

            string Message = "";
            bool res = false;
            int classValue = (int)Enum.Parse(typeof(ClassName), StudentClass);
            try
            {
                DateTime parsedDateTime;
                if (!DateTime.TryParse(IssueDateTime, out parsedDateTime))
                {
                    return Ok(new { message = "Invalid Date Format" });
                }

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_BookIssueToStudent", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FullName", FullName);
                        cmd.Parameters.AddWithValue("@StudentClass", classValue);
                        cmd.Parameters.AddWithValue("@IssueDateTime", parsedDateTime);
                        cmd.Parameters.AddWithValue("@HdnStudentId", HdnStudentId);
                        cmd.Parameters.AddWithValue("@BookId", BookId);
                        cmd.Parameters.AddWithValue("@BookName", hdnBookName);
                        cmd.Parameters.AddWithValue("@BookAuthor", hdnBookAuthor);
                        connection.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                res = true;
                Message = "New Book Issued successfully done";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Ok(new { message = Message });
        }


        [HttpGet]
        public IActionResult CheckIssuedBookAPI()
        {
            string Message = "";
            bool res = false;
            List<Student> ObjIssedBook = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ViewIssuedBookToStudents", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Student libStudent = new Student();
                            libStudent.BookName = Convert.ToString(reader["Book1"]);
                            libStudent.BookAuthorName = Convert.ToString(reader["Book1Publisher"]);
                            if (Enum.TryParse<BookMedium>(Convert.ToString(reader["Book1Medium"]), out BookMedium BookMediumLanguage))
                            {
                                libStudent.BookMediumLanguage = BookMediumLanguage;
                            }
                            libStudent.StudentName = Convert.ToString(reader["Book1IssuedTo"]);
                            if (Enum.TryParse<ClassName>(Convert.ToString(reader["Book1IssueClass"]), out ClassName classname))
                            {
                                libStudent.Class = classname;
                            }
                            ObjIssedBook.Add(libStudent);
                            res = true;
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Ok(ObjIssedBook);
        }


        [HttpPost("{HdnBookId}")]
        public IActionResult SubmitBookAPI(int HdnBookId, int HdnStudentId)
        {
            bool res = false;
            string Message = "";

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SP_SubmitIssueBook", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HdnBookId", HdnBookId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                res = true;
                Message = "Book Sumbitted Successfully";

            }
            catch (Exception ex)
            {

            }
            return Ok(new { message = Message });
        }

        [HttpGet("{Id}")]
        public IActionResult updateBookAPI(int Id)
        {
            bool False;
            string Message = "";

            try
            {
                Student objstudent = new Student();

                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("Sp_UpdateBookDataGet", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            Id = Convert.ToInt32(rdr["Id"]);
                            objstudent.BookName = Convert.ToString(rdr["Book1"]);
                            objstudent.BookAuthorName = Convert.ToString(rdr["Book1Publisher"]);
                            objstudent.BookMediumLanguage = (BookMedium)Enum.Parse(typeof(BookMedium), Convert.ToString(rdr["Book1Medium"]));

                        }
                    }

                }
                return Ok(objstudent);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

        }


        [HttpGet]
        public IActionResult PostUpdateBookAPI(string BookName, string BookAuthorName, string BookMedium, int BookId)
        {
            bool res = false;
            string Message = "";
            int BookMediumLang = (int)Enum.Parse(typeof(BookMedium), BookMedium);
            try
            {
                Student pStudent = new Student();
                pStudent.BookName = BookName;
                pStudent.BookAuthorName = BookAuthorName;
                pStudent.BookMediumLanguage = (BookMedium)BookMediumLang;
                pStudent.Id = BookId;


                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_BookUpdate", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", BookId);
                        cmd.Parameters.AddWithValue("@BookName", BookName);
                        cmd.Parameters.AddWithValue("@BookAuthorName", BookAuthorName);
                        cmd.Parameters.AddWithValue("@BookMedium", BookMediumLang);
                        cmd.ExecuteNonQuery();

                    }
                    Message = "Book Updated Successfully in the Library Record";
                    res = true;
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }

            return Ok(new { msg = Message });
        }

        [HttpDelete("{Id}")]
        public IActionResult DeleteBookPostAPI(int Id)
        {
            bool res = false;
            string Message = "";
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_DeleteLibBook", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@BookId", Id);
                        cmd.ExecuteNonQuery();
                    }
                    res = true;
                    Message = "Book Deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Ok(new { msg = Message });

        }


        #region "Get Library Data"
        [HttpGet]
        public IActionResult GetLibraryData()
        {
            List<Student> ltrStudents = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("LibraryCount", con);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student objStudent = new Student();
                            if (Enum.TryParse<BookMedium>(Convert.ToString(rdr["Book1Medium"]), out BookMedium BookMediumLanguage))
                            {
                                objStudent.BookMediumLanguage = BookMediumLanguage;
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
