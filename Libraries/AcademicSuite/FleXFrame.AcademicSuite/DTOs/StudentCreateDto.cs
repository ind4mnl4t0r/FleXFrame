using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.DTOs
{
    public class StudentCreateDto
    {
        public enum Genders { Male, Female }

        public required string StudentID { get; set; }
        public required string FirstName { get; set; }
        public string? MiddleName { get; set; }
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

        public required Genders Gender { get; set; }
        public string? NationalIDNumber { get; set; }


    }
}
