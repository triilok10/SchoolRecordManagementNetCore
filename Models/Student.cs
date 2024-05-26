
using System.ComponentModel.DataAnnotations;


namespace CoreProject1.Models
{



    public class Student
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter your FirstName")]
        public string StudentName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Select your Class")]
        public ClassName Class { get; set; }

        [Required(ErrorMessage = "Please Select your Gender")]
        public GenderType Gender { get; set; }

        [Required(ErrorMessage = "Please Enter your Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter your Father's Name")]
        public string FatherName { get; set; }

        [Required(ErrorMessage = "Please Enter your Mother's Name")]
        public string MotherName { get; set; }

        [Required(ErrorMessage = "Please Enter the Remarks")]
        public string Remarks { get; set; }

        [Required(ErrorMessage = "Please Enter the Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please Enter the Mobile")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Please Select the Profile Photo")]
        public string Filepath { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string BookName { get; set; }

        public string BookAuthorName { get; set; }
        public int HdnStudentId { get; set; }
        public string IssueDateTime { get; set; }
        public BookMedium BookMediumLanguage { get; set; }
    }

    public enum BookMedium
    {
        Hindi, 
        English, 
        Punjabi,
        Spanish,
        Italian,
        Other
    }
    public enum ClassName
    {
        First,
        Second,
        Third,
        Fourth,
        Fifth,
        Sixth,
        Seventh,
        Eight,
        Nineth,
        Tenth,
        Eleventh,
        Twelveth
    }
    public enum GenderType
    {
        Male,
        Female,
        Other
    }
}