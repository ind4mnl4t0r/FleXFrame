using FleXFrame.AcademicSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Interfaces.IRepositories
{
    public interface IStudentEnrollmentRepository
    {
        Task<StudentEnrollment?> GetStudentEnrollmentByIDAsync(string studentEnrollmentID);
        Task<IEnumerable<StudentEnrollment>> GetAllStudentEnrollmentsAsync();
        Task<Student?> GetLastAddedStudentEnrollmentAsync();
        Task AddStudentEnrollmentAsync(StudentEnrollment studentEnrollment);
        Task UpdateStudentEnrollmentAsync(StudentEnrollment studentEnrollment);
        Task DeleteStudentEnrollmentAsync(string studentEnrollmentID);
    }
}
