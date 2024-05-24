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
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    FatherName = reader["Fathername"].ToString(),
                                    MotherName = reader["Mothername"].ToString(),
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
            try
            {
                DateTime parsedDateTime;
                if (!DateTime.TryParse(IssueDateTime, out parsedDateTime))
                {
                    Message = "Invalid Date Format";
                }
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    //string query = "Update Library Set Book1IssuedTo= @Book1IssuedTo , Book1IssueClass = @Book1IssueClass ,Book1IssueDateTime = @Book1IssueDateTime,Book1IssueId = @Book1IssueId WHERE Id = @id";
                    string query = @"UPDATE Library 
                                     SET 
                                        Book1IssuedTo = CASE WHEN Book1IssuedTo IS NULL THEN @Book1IssuedTo ELSE Book1IssuedTo END,
                                        Book1IssueClass = CASE WHEN Book1IssueClass IS NULL THEN @Book1IssueClass ELSE Book1IssueClass END,
                                        Book1IssueDateTime = CASE WHEN Book1IssueDateTime IS NULL THEN @Book1IssueDateTime ELSE Book1IssueDateTime END,
                                        Book1IssueId = CASE WHEN Book1IssueId IS NULL THEN @Book1IssueId ELSE Book1IssueId END
                                     WHERE Id = @Id AND (Book1IssuedTo IS NULL OR Book1IssueClass IS NULL OR Book1IssueDateTime IS NULL OR Book1IssueId IS NULL)";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", BookId);
                        cmd.Parameters.AddWithValue("@Book1IssuedTo", FullName);
                        cmd.Parameters.AddWithValue("@Book1IssueClass", StudentClass);
                        cmd.Parameters.AddWithValue("@Book1IssueDateTime", parsedDateTime);
                        cmd.Parameters.AddWithValue("@Book1IssueId", HdnStudentId);
                        cmd.ExecuteNonQuery();
                    }
                    Message = "Book Issued Successfully to the Student";
                    res = true;
                    if (res == true)
                    {
                        string Studentquery = "Update Student Set Book1= @Book1 , BAuthor1 = @BAuthor1 WHERE Id = @id";
                        using (SqlCommand cmd = new SqlCommand(Studentquery, connection))
                        {
                            cmd.Parameters.AddWithValue("@Id", HdnStudentId);
                            cmd.Parameters.AddWithValue("@Book1", hdnBookName);
                            cmd.Parameters.AddWithValue("@BAuthor1", hdnBookAuthor);
                            cmd.ExecuteNonQuery();
                            Message = "Book Inseration Update in Student Record";
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }


            return Ok();
        }
    }
}
