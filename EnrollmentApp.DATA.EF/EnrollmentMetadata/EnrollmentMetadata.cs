using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrollmentApp.DATA.EF//.EnrollmentMetadata
{
        #region Enrollment Metadata

    public class EnrollmentMetadata
    {
        //[Required]
        //public int EnrollmentID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [Required]
        public int ScheduledClassID { get; set; }

        [Required]
        public DateTime EnrollmentDate { get; set; }

        

        

    }
    public partial class Enrollment
    {

    }
    #endregion

    #region Cours

    public class CoursMetadata{
        //[Required]
        //public int CourseID { get; set; }

        [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
        [Required]
        public string CourseName { get; set; }

        [Required]
        public string CourseDescription { get; set; }

        [Required]
        public byte CreditHours { get; set;}

        [StringLength(250, ErrorMessage = "Value must be 250 characters or less")]
        public string Curriculum { get; set; }

        [StringLength(500, ErrorMessage = "Value must be 500 characters or less")]
        public string Notes { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
    public partial class Cours
    {

    }
    #endregion
    #region Scheduled Class
    public class ScheduledClassMetadata
    {
        //[Required]
        //public int ScheduledClassID { get; set; }

        [Required]
        public int CourseID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime StartDate { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime EndDate { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "Value must be 40 characters or less")]
        public string InstructorName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Value must be 20 characters or less")]
        public string Location { get; set; }

        [Required]
        public int SCSID { get; set; }
    }

    //[MetadataType(typeof(CoursMetadata))]
    public partial class ScheduledClass
    {

        public string ClassInfo
        {
            get { return StartDate + " " + Cours.CourseName + " " + Location; }
        }

    }
    #endregion

        #region ScheduledClassStatus
    public class ScheduledClassStatusMetadata
    {
        //[Required]
        //public int SCSID { get; set; }

        [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
        [Required]
        public string SCSName { get; set; }
    }
    public partial class ScheduledClassStatus
    {


    }
    #endregion

    #region Student
    public class StudentMetadata
    {
        //[Required]
        //public int StudentID { get; set; }

        [StringLength(20, ErrorMessage = "Value must be 20 characters or less")]
        [Required]
        public string FirstName { get; set; }

        [StringLength(20, ErrorMessage = "Value must be 20 characters or less")]
        [Required]
        public string LastName { get; set; }

        [StringLength(15, ErrorMessage = "Value must be 15 characters or less")]
        public string Major { get; set; }

        [StringLength(50, ErrorMessage = "Value must be 50 characters or less")]
        public string Address { get; set; }

        [StringLength(25, ErrorMessage = "Value must be 25 characters or less")]
        public string City { get; set; }

        [StringLength(2, ErrorMessage = "Value must be 2 characters or less")]
        public string State { get; set; }

        [StringLength(10, ErrorMessage = "Value must be 10 characters or less")]
        public string Zip { get; set; }

        [StringLength(13, ErrorMessage = "Value must be 13 characters or less")]
        public string Phone { get; set; }

        [Required]
        [StringLength(60, ErrorMessage = "Value must be 60 characters or less")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "Value must be 100 characters or less")]
        public string PhotoUrl { get; set; }

        [Required]
        public int SSID { get; set; }

    }
    public partial class Student
    {
        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
    #endregion

    #region StudentStatus
    public class StudentStatusMetadata
    {
        [Required]
        public int SSID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Value must be 30 characters or less")]
        public string SSName { get; set; }

        [StringLength(250, ErrorMessage = "Value must be 250 characters or less")]
        public string SSDescription { get; set; }
    }
    public partial class StudentStatus
    {

    }
    #endregion
}
