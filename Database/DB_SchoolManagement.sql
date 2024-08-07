USE [master]
GO
/****** Object:  Database [SchoolManagementSystem]    Script Date: 13-07-2024 08:40:02 ******/
CREATE DATABASE [SchoolManagementSystem]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolManagementSystem', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.CYNOSUREDBS\MSSQL\DATA\SchoolManagementSystem.mdf' , SIZE = 24576KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SchoolManagementSystem_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.CYNOSUREDBS\MSSQL\DATA\SchoolManagementSystem_log.ldf' , SIZE = 24576KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SchoolManagementSystem] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolManagementSystem].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolManagementSystem] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolManagementSystem] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolManagementSystem] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SchoolManagementSystem] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolManagementSystem] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchoolManagementSystem] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolManagementSystem] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolManagementSystem] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolManagementSystem] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolManagementSystem] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SchoolManagementSystem] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SchoolManagementSystem] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SchoolManagementSystem] SET QUERY_STORE = OFF
GO
USE [SchoolManagementSystem]
GO
/****** Object:  Table [dbo].[FeeReport]    Script Date: 13-07-2024 08:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FeeReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StudentId] [int] NULL,
	[StudentClass] [int] NULL,
	[StudentFee] [int] NULL,
	[StudentName] [varchar](30) NULL,
	[StudentFatherName] [varchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Library]    Script Date: 13-07-2024 08:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Library](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Book1] [varchar](50) NULL,
	[Book1Publisher] [varchar](50) NULL,
	[Book1IssuedTo] [varchar](50) NULL,
	[Book1IssueClass] [varchar](50) NULL,
	[Book1IssueDateTime] [date] NULL,
	[Book1IssueId] [int] NULL,
	[Book1Medium] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STUDENT]    Script Date: 13-07-2024 08:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STUDENT](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FIRSTNAME] [varchar](30) NOT NULL,
	[LASTNAME] [varchar](30) NULL,
	[FATHERNAME] [varchar](30) NOT NULL,
	[MOTHERNAME] [varchar](30) NOT NULL,
	[ADDRESS] [varchar](400) NOT NULL,
	[REMARK] [varchar](400) NOT NULL,
	[EMAIL] [varchar](50) NOT NULL,
	[GENDER] [int] NOT NULL,
	[CLASS] [int] NOT NULL,
	[FILEPATH] [varchar](400) NOT NULL,
	[MOBILE] [varchar](14) NOT NULL,
	[Book1] [varchar](50) NULL,
	[BAuthor1] [varchar](50) NULL,
	[DateOfBirth] [date] NULL,
	[FeeReport] [int] NULL,
	[IsFeePaid] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TeacherDetails]    Script Date: 13-07-2024 08:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TeacherDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](30) NOT NULL,
	[LastName] [varchar](30) NULL,
	[FathersName] [varchar](30) NOT NULL,
	[MotherName] [varchar](30) NOT NULL,
	[Address] [varchar](400) NOT NULL,
	[Remarks] [varchar](400) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Gender] [int] NOT NULL,
	[Class] [int] NOT NULL,
	[Filepath] [varchar](400) NOT NULL,
	[Mobile] [varchar](14) NOT NULL,
	[DateOfBirth] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[STUDENT] ADD  DEFAULT ('0') FOR [IsFeePaid]
GO
/****** Object:  StoredProcedure [dbo].[AddBooksInLib]    Script Date: 13-07-2024 08:40:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddBooksInLib]

    @Book1 VARCHAR(50),
    @Book1Publisher VARCHAR(50),
	@Book1Medium int
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Library(  Book1,Book1Publisher,Book1Medium)
    VALUES (  @Book1, @Book1Publisher, @Book1Medium);
END
GO
/****** Object:  StoredProcedure [dbo].[AddStudentMain]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddStudentMain]  
    @FirstName VARCHAR(30),  
    @LastName VARCHAR(30),  
    @Class int,  
    @Gender int,  
    @FatherName VARCHAR(30),  
    @MotherName VARCHAR(30),  
    @Address VARCHAR(400),  
    @Remark VARCHAR(400),  
 @Email  VARCHAR(50),  
 @Mobile VARCHAR(13),  
 @Filepath VARCHAR(400),  
 @DateOfBirth VARCHAR(30),
 @FeeReport VARCHAR(6)
AS  
BEGIN  
    SET NOCOUNT ON;  
    INSERT INTO Student( FirstName, LastName, Class, Gender, FatherName, MotherName, Address, Remark,Email,Mobile, Filepath, DateOfBirth ,FeeReport)  
    VALUES ( @FirstName, @LastName, @Class, @Gender, @FatherName, @MotherName, @Address, @Remark,@Email,@Mobile, @Filepath,@DateOfBirth, @FeeReport);  
END


GO
/****** Object:  StoredProcedure [dbo].[AddTeacherMain]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddTeacherMain]
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @Class int,
    @Gender int,
    @FathersName VARCHAR(30),
    @MotherName VARCHAR(30),
    @Address VARCHAR(400),
    @Remark VARCHAR(400),
	@Email  VARCHAR(50),
	@Mobile VARCHAR(13),
	@Filepath VARCHAR(400),
	@DateOfBirth Date
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TeacherDetails( FirstName, LastName, Class, Gender, FathersName, MotherName, Address, Remarks,Email,Mobile, Filepath , DateOfBirth)
    VALUES ( @FirstName, @LastName, @Class, @Gender, @FathersName, @MotherName, @Address, @Remark,@Email,@Mobile, @Filepath , @DateofBirth);
END
GO
/****** Object:  StoredProcedure [dbo].[AddViewStudents]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddViewStudents]
AS
BEGIN
    SELECT ID,
           FirstName,
           LastName,
           FatherName,
           MotherName,
           Address,
           Remark,
           Gender,
           Class,
          Mobile,
		  Email,
		  FILEPATH
    FROM Student; 
END
GO
/****** Object:  StoredProcedure [dbo].[AddViewTeachers]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddViewTeachers]
AS
BEGIN
    SELECT ID,
           FirstName,
           LastName,
           FathersName,
           MotherName,
           Address,
           Remarks,
           Gender,
           Class,
           Mobile,
           Email,
           FILEPATH,
           DateOfBirth
    FROM TeacherDetails; 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteStudentByID]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteStudentByID]
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Check if the student has paid the fee
    DECLARE @IsFeePaid BIT;

    SELECT @IsFeePaid = IsFeePaid
    FROM Student
    WHERE Id = @ID;

    -- If IsFeePaid is 0, throw an error
    IF @IsFeePaid = 0
    BEGIN
        BEGIN TRY
            THROW 52000, 'Please pay the dues to delete the record of the student.', 1;
            RETURN; -- Exit procedure
        END TRY
        BEGIN CATCH
            THROW; -- Throw the caught exception
        END CATCH
    END

    -- Check if the student has a library book that needs to be submitted
    IF EXISTS (
        SELECT 1
        FROM Student
        WHERE Id = @ID
            AND (Book1 IS NOT NULL OR BAuthor1 IS NOT NULL) -- Adjust conditions as per your schema
    )
    BEGIN
        BEGIN TRY
            -- If a library book is not submitted, throw an error
            THROW 51000, 'Please submit the library book to delete the record of the student.', 1;
            RETURN; -- Exit procedure
        END TRY
        BEGIN CATCH
            THROW; -- Throw the caught exception
        END CATCH
    END

    -- If no errors occurred, proceed with deleting the student record
    DELETE FROM Student
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTeacherByID]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTeacherByID]
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM [dbo].[TeacherDetails]
    WHERE [ID] = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[GetEDOC]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetEDOC]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        ID,
        FirstName,
        LastName,
        FatherName,
        MotherName,
        Address,
        Remark,
        Mobile,
        Filepath,
        Gender,
        Class
    FROM 
			STUDENT
    WHERE 
        ID = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[LibraryCount]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[LibraryCount]
AS
BEGIN
Select * from Library
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BookIssueToStudent]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BookIssueToStudent]
    @FullName NVARCHAR(100),
    @StudentClass NVARCHAR(50),
    @IssueDateTime DATETIME,
    @HdnStudentId INT,
    @BookId INT,
    @BookName NVARCHAR(100),
    @BookAuthor NVARCHAR(100)
AS
BEGIN
    -- Check if the book is already issued to someone else in the Library table
    IF EXISTS (
        SELECT 1
        FROM Library
        WHERE Id = @BookId
            AND (Book1IssueId IS NOT NULL 
                 OR Book1IssueDateTime IS NOT NULL 
                 OR Book1IssueClass IS NOT NULL 
                 OR Book1IssuedTo IS NOT NULL)
    )
    BEGIN
        THROW 51000, 'This book is already issued to someone else.', 1;
        RETURN;
    END

    -- Check if Book1 and BAuthor1 columns are NULL in the Student table
    IF EXISTS (
        SELECT 1
        FROM Student
        WHERE Id = @HdnStudentId
            AND (Book1 IS NOT NULL OR BAuthor1 IS NOT NULL)
    )
    BEGIN
        THROW 51000, 'Please submit the last book, then only you can issue a new book.', 1;
        RETURN;
    END

    -- Update Library table if the book is available
    BEGIN TRY
        -- Update Library table if the book is available
        UPDATE Library
        SET Book1IssueId = @HdnStudentId,
            Book1IssueDateTime = @IssueDateTime,
            Book1IssueClass = @StudentClass,
            Book1IssuedTo = @FullName
        WHERE Id = @BookId;

        -- Update Student table
        UPDATE Student
        SET Book1 = @BookName,
            BAuthor1 = @BookAuthor
        WHERE Id = @HdnStudentId;

        PRINT 'Book is available for issue.';
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_BookUpdate]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_BookUpdate]
@Id int,
@BookName Varchar(30),
@BookAuthorName Varchar(30),
@BookMedium Varchar(10)
AS 
BEGIN
IF EXISTS (
        SELECT 1
        FROM Library
        WHERE Id = @Id
            AND (Book1IssueId IS NOT NULL 
                 OR Book1IssueDateTime IS NOT NULL 
                 OR Book1IssueClass IS NOT NULL 
                 OR Book1IssuedTo IS NOT NULL)
    )
    BEGIN
        THROW 51000, 'Please Submit this Book to Update', 1;
        RETURN;
    END
	BEGIN TRY
    UPDATE Library
    SET
        Book1 = @BookName,
        Book1Publisher = @BookAuthorName,
        Book1Medium = @BookMedium
        
    WHERE Id = @Id;
	END TRY
    BEGIN CATCH
        THROW;
    END CATCH
End


GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteLibBook]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[SP_DeleteLibBook]
@BookId int 
AS
  BEGIN 
  IF EXISTS (
        SELECT 1
        FROM Library
        WHERE Id = @BookId
            AND (Book1IssueId IS NOT NULL 
                 OR Book1IssueDateTime IS NOT NULL 
                 OR Book1IssueClass IS NOT NULL 
                 OR Book1IssuedTo IS NOT NULL)
    )
    BEGIN
        THROW 51000, 'Cannot Deleted. This book is issued from the Library, Please submit this book to perform Delete action', 1;
        RETURN;
    END

	BEGIN TRY
    SET NOCOUNT ON;
    DELETE FROM Library
    WHERE Id = @BookId;
	END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[SP_FeeSubmitSet]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_FeeSubmitSet]
  @StudentId INT,
  @StudentClass INT,
  @StudentFee INT,
  @StudentName VARCHAR(30),
  @StudentFatherName VARCHAR(30)
AS
BEGIN
  BEGIN TRY
    BEGIN TRANSACTION; 
    
    DECLARE @IsFeePaid BIT;

    -- Check if fee is already paid for the student
    SELECT @IsFeePaid = IsFeePaid
    FROM Student
    WHERE ID = @StudentId;

    IF @IsFeePaid = 1
    BEGIN
      -- If fee is already paid, rollback transaction and return message
      THROW 51000, 'Student Fee already paid', 1;
    END
    ELSE
    BEGIN
      -- If fee is not paid, insert into FeeReport and update Student table
      INSERT INTO FeeReport (StudentId, StudentClass, StudentFee, StudentName, StudentFatherName)
      VALUES (@StudentId, @StudentClass, @StudentFee, @StudentName, @StudentFatherName);

      UPDATE Student
      SET IsFeePaid = 1
      WHERE ID = @StudentId;
    END

    COMMIT TRANSACTION;
  END TRY
  BEGIN CATCH
    IF @@TRANCOUNT > 0
      ROLLBACK TRANSACTION;
      
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
  END CATCH;
END;
GO
/****** Object:  StoredProcedure [dbo].[SP_Sportsclub]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SP_Sportsclub]
AS
BEGIN
SELECT ID, FIRSTNAME FROM STUDENT
END
GO
/****** Object:  StoredProcedure [dbo].[SP_SubmitIssueBook]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SP_SubmitIssueBook] 
@HdnBookId INT 
AS
BEGIN 
SET NOCOUNT ON; 
DECLARE @StudentId INT; 

SELECT @StudentId = Book1IssueId
FROM Library WHERE Id = @HdnBookId;


UPDATE Library SET book1Issuedto = NULL, 
Book1IssueClass = NULL, 
Book1issueDateTime = NULL,
Book1IssueId = Null
WHERE Id = @HdnBookId; 

UPDATE Student SET Book1 = NULL, BAuthor1 = NULL
WHERE Id = @StudentId; 
END;
GO
/****** Object:  StoredProcedure [dbo].[Sp_UpdateBookDataGet]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROC [dbo].[Sp_UpdateBookDataGet]
 (
 @Id int
 )
 AS BEGIN SELECT * FROM Library where id = @Id
 End
GO
/****** Object:  StoredProcedure [dbo].[Sp_ViewBooks]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Sp_ViewBooks]
AS
BEGIN
    SELECT ID,
           Book1,
		   Book1Publisher,
		   Book1Medium
    FROM Library; 
END
GO
/****** Object:  StoredProcedure [dbo].[Sp_ViewIssuedBookToStudents]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[Sp_ViewIssuedBookToStudents]
AS
BEGIN
    Select * from Library Where LEN(RTRIM(BOOK1IssueId))>0;

END
GO
/****** Object:  StoredProcedure [dbo].[StudentCount]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[StudentCount]
AS
BEGIN
SELECT * FROM Student
END
GO
/****** Object:  StoredProcedure [dbo].[TeacherCount]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TeacherCount]
AS
BEGIN
SELECT * FROM TeacherDetails
END
GO
/****** Object:  StoredProcedure [dbo].[UpChangeData]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpChangeData] 
 (
 @Id int
 )
 AS BEGIN SELECT * FROM TeacherDetails where id = @Id
 End
GO
/****** Object:  StoredProcedure [dbo].[UpdateChangeData]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[UpdateChangeData] 
 (
 @Id int
 )
 AS BEGIN SELECT * FROM Student where id = @Id
 End
GO
/****** Object:  StoredProcedure [dbo].[UpdateStudentData]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateStudentData]
    @Id INT,
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @FatherName VARCHAR(30),
    @MotherName VARCHAR(30),
    @Gender VARCHAR(10),
    @Address VARCHAR(400),
    @Class VARCHAR(400),
	@Mobile VARCHAR(14),
	@Email VARCHAR(50),
	@Remark VARCHAR(400),
	@Filepath VARCHAR(400),
	@DateOfBirth VARCHAR(30)
AS
BEGIN
    UPDATE Student
    SET
        FirstName = @FirstName,
        LastName = @LastName,
        FatherName = @FatherName,
        MotherName = @MotherName,
        Gender = @Gender,
        Address = @Address,
        Class = @Class,
		Mobile = @Mobile,
		Email = @Email,
		Remark = @Remark,
		Filepath = @Filepath,
		DateOfBirth = @DateOfBirth
    WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTeacherData]    Script Date: 13-07-2024 08:40:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTeacherData]
    @Id INT,
    @FirstName VARCHAR(30),
    @LastName VARCHAR(30),
    @FatherName VARCHAR(30),
    @MotherName VARCHAR(30),
    @Gender VARCHAR(10),
    @Address VARCHAR(400),
    @Class VARCHAR(400),
	@Mobile VARCHAR(14),
	@Email VARCHAR(50),
	@Remark VARCHAR(400),
	@Filepath VARCHAR(400),
	@DateOfBirth Date
AS
BEGIN
    UPDATE TeacherDetails
    SET
        FirstName = @FirstName,
        LastName = @LastName,
        FathersName = @FatherName,
        MotherName = @MotherName,
        Gender = @Gender,
        Address = @Address,
        Class = @Class,
		Mobile = @Mobile,
		Email = @Email,
		Remarks = @Remark,
		Filepath = @Filepath,
		DateOfBirth =@DateOfBirth
    WHERE Id = @Id;
END
GO
USE [master]
GO
ALTER DATABASE [SchoolManagementSystem] SET  READ_WRITE 
GO
