using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Core.Models
{
    public class Student
    {
        public enum Genders { Male, Female }

        // Personal Information
        [MaxLength(50)]
        public required string StudentID { get; set; }
        [MaxLength(254)]
        public required string FirstName { get; set; }
        [MaxLength(254)]
        public string? MiddleName { get; set; }
        [MaxLength(254)]
        public required string LastName { get; set; }

        public string NameWithInitial
        {
            get
            {
                // Split the FirstName into parts (split by space)
                string[] firstNameParts = FirstName?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
                string[] middleNameParts = !string.IsNullOrEmpty(MiddleName) ? MiddleName.Split(' ', StringSplitOptions.RemoveEmptyEntries) : Array.Empty<string>();

                string initials = string.Join(".", firstNameParts.Select(part => part[0] + "."));
                initials += string.Join(".", middleNameParts.Select(part => part[0] + "."));

                return $"{initials}{LastName}".Trim();
            }
        }

        public required DateTime DateOfBirth { get; set; }
        public required Genders Gender { get; set; }

        [MaxLength(20)]
        public string? NationalIDNumber { get; set; }


        // Contact Information
        [MaxLength(254)]
        public string? Email { get; set; }
        [MaxLength(16)]
        public string? PhoneNumber { get; set; }
        [MaxLength(500)]
        public required string Address { get; set; }
        [MaxLength(254)]
        public required string City { get; set; }
        [MaxLength(254)]
        public string? State { get; set; }
        [MaxLength(20)]
        public string? PostalCode { get; set; }
        [MaxLength(254)]
        public required string Country { get; set; }


        // Guardian Information
        [MaxLength(254)]
        public string? GuardianName { get; set; }
        [MaxLength(16)]
        public string? GuardianPhoneNumber { get; set; }
        [MaxLength(254)]
        public string? GuardianEmail { get; set; }
        [MaxLength(254)]
        public string? RelationshipToStudent { get; set; }


        // Other Details
        [MaxLength(2048)]
        public string? PhotoUrl { get; set; }
        [MaxLength(2000)]
        public string? Notes { get; set; }
        [MaxLength(16)]
        public string? EmergencyContactMobile { get; set; }
        [MaxLength(16)]
        public string? EmergencyContactPhone { get; set; }

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
