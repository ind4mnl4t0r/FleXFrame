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
        [Key]
        [Required]
        [MaxLength(20)]
        public required string CourseID { get; set; }

        [Required]
        [MaxLength(254)]
        public required string CourseName { get; set; }


    }
}
