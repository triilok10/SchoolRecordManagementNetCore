using System;
using System.Collections.Generic;

namespace CoreProject1.Models;

public partial class Student
{
    public int Id { get; set; }

    public string Firstname { get; set; } = null!;

    public string? Lastname { get; set; }

    public string Fathername { get; set; } = null!;

    public string Mothername { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Remark { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int Gender { get; set; }

    public int Class { get; set; }

    public string Filepath { get; set; } = null!;

    public string Mobile { get; set; } = null!;
}
