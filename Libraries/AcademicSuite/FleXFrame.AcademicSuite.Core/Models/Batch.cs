using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Core.Models
{
    public class Batch
    {
        public enum BatchStatuses { Open, Close, Complete }

        // Batch Information
        [MaxLength(50)]
        public required string BatchID { get; set; }
        [MaxLength(50)]
        public required string CourseID { get; set; }
        [MaxLength(50)]
        public string? Description { get; set; }
        public required DateTime PlannedStartDate { get; set; }
        public required DateTime PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public required BatchStatuses BatchStatus { get; set; }
        [MaxLength(2000)]
        public string? Note { get; set; }


        // Audit Fields
        public required DateTime DateCreated { get; set; }
        [MaxLength(254)]
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        [MaxLength(254)]
        public string? ModifiedBy { get; set; }


        // Navigation Properties
        public required virtual Course Course { get; set; }

    }
}
