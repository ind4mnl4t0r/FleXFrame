using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Core.Models
{
    public class Course
    {
        public enum CourseTypes { FullTime, PartTime }
        public enum CourseStatuses { Active, Inactive, Suspended, Deleted }

        // Coure Information
        [MaxLength(50)]
        public required string CourseID { get; set; }
        [MaxLength(254)]
        public required string CourseName { get; set; }
        [MaxLength(2000)]
        public string? Description { get; set; }
        public required CourseTypes CourseType { get; set; }
        public required CourseStatuses CourseStatus { get; set; }
        [MaxLength(2)]
        public required int DurationInMonths { get; set; }


        // Audit Fields
        public required DateTime DateCreated { get; set; }
        [MaxLength(254)]
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        [MaxLength(254)]
        public string? ModifiedBy { get; set; }


        public virtual ICollection<StudentEnrollment> StudentEnrollments { get; set; } = new List<StudentEnrollment>();
    }
}
