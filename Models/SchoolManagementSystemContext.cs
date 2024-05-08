using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CoreProject1.Models;

public partial class SchoolManagementSystemContext : DbContext
{
    public SchoolManagementSystemContext()
    {
    }

    public SchoolManagementSystemContext(DbContextOptions<SchoolManagementSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TeacherDetail> TeacherDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACER\\CYNOSUREDBS;Database=SchoolManagementSystem;Integrated Security=True;TrustServerCertificate=True;");

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__STUDENT__3214EC273A510D2F");

            entity.ToTable("STUDENT");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Class).HasColumnName("CLASS");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fathername)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("FATHERNAME");
            entity.Property(e => e.Filepath)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("FILEPATH");
            entity.Property(e => e.Firstname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("FIRSTNAME");
            entity.Property(e => e.Gender).HasColumnName("GENDER");
            entity.Property(e => e.Lastname)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("LASTNAME");
            entity.Property(e => e.Mobile)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("MOBILE");
            entity.Property(e => e.Mothername)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("MOTHERNAME");
            entity.Property(e => e.Remark)
                .HasMaxLength(400)
                .IsUnicode(false)
                .HasColumnName("REMARK");
        });

        modelBuilder.Entity<TeacherDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TeacherD__3214EC271E2A2114");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FathersName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Filepath)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(14)
                .IsUnicode(false);
            entity.Property(e => e.MotherName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Remarks)
                .HasMaxLength(400)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
