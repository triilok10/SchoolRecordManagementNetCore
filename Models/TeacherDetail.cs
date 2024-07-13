using System.ComponentModel.DataAnnotations;


namespace CoreProject1.Models
{
    public class TeacherDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter your FirstName")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string hdnSubject { get; set; }
        public string hdnGender { get; set; }

        [Required(ErrorMessage = "Please Select your Class")]
        public Degination Subject { get; set; }

        [Required(ErrorMessage = "Please Select your Gender")]
        public GenderTypes Gender { get; set; }

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

    }
    public enum Degination
    {
        PTI,
        TGTEnglish,
        TGTBiology,
        TGTPhysics,
        TGTChemistry,
        TGTMath,
        TGTSocialScience,
        TGTComputer,
        TGTART,
        TGTHindi,
        PGTEnglish,
        PGTBiology,
        PGTPhysics,
        PGTChemistry,
        PGTMath,
        PGTSocialScience,
        PGTComputer,
        PGTART,
        PGTHindi
    }
    public enum GenderTypes
    {
        Male,
        Female,
        Other
    }
}
