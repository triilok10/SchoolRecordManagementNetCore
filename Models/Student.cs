
using System.ComponentModel.DataAnnotations;


namespace CoreProject1.Models
{



    public class Student
    {
        public int Id { get; set; }

        public string? StudentName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }


        public GenderType? Gender { get; set; }

        public string? hdnGender { get; set; }
        public string? Address { get; set; }

        public string? FatherName { get; set; }

        public string? MotherName { get; set; }

        public string? Remarks { get; set; }

        public string? Email { get; set; }
        public string? Mobile { get; set; }

        public string? Filepath { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? BookName { get; set; }

        public string? BookAuthorName { get; set; }
        public int? HdnStudentId { get; set; }
        public int? FeeAmount { get; set; }
        public int? FirstInstallment { get; set; }
        public int? SecondInstallment { get; set; }
        public string? IssueDateTime { get; set; }
        public BookMedium? BookMediumLanguage { get; set; }
        public bool? IsFeePaid { get; set; }
        public ClassName? Class { get; set; }
        public string? hdnClass { get; set; }
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