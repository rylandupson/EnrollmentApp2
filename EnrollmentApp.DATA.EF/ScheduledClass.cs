//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EnrollmentApp.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScheduledClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduledClass()
        {
            this.Enrollments = new HashSet<Enrollment>();
        }
    
        public int ScheduledClassID { get; set; }
        public int CourseID { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string InstructorName { get; set; }
        public string Location { get; set; }
        public int SCSID { get; set; }
    
        public virtual Cours Cours { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual ScheduledClassStatus ScheduledClassStatus { get; set; }
    }
}
