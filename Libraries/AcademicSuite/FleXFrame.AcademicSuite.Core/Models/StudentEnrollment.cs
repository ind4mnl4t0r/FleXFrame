using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Core.Models
{
    public class StudentEnrollment
    {
        // Batch Information
        [MaxLength(50)]
        public required string EnrollmentID { get; set; }
        [MaxLength(50)]
        public required string StudentID { get; set; }
        [MaxLength(50)]
        public required string CourseID { get; set; }
        [MaxLength(50)]
        public required string BatchID { get; set; }
        public required DateTime EnrollmentDate { get; set; }


        // Audit Fields
        public required DateTime DateCreated { get; set; }
        [MaxLength(254)]
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        [MaxLength(254)]
        public string? ModifiedBy { get; set; }


        // Navigation Properties
        public required virtual Student Student { get; set; }
        public required virtual Course Course { get; set; }
        public required virtual Batch Batch { get; set; }
    }
}
