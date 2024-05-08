using System;
using System.Collections.Generic;

namespace CoreProject1.Models;

public partial class TeacherDetail
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string FathersName { get; set; } = null!;

    public string MotherName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Remarks { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Gender { get; set; }

    public int Class { get; set; }

    public string Filepath { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }
}
