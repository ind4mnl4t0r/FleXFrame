using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Core.Models
{
    public class CourseBatch
    {
        [MaxLength(50)]
        public required string CourseBatchID { get; set; }
        [MaxLength(50)]
        public required string CourseID { get; set; }
        [MaxLength(50)]
        public required string BatchID { get; set; }

        // Audit Fields
        public required DateTime DateCreated { get; set; }
        [MaxLength(254)]
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        [MaxLength(254)]
        public string? ModifiedBy { get; set; }


        // Navigation properties
        public required virtual Course Course { get; set; }
        public required virtual Batch Batch { get; set; }
    }
}
