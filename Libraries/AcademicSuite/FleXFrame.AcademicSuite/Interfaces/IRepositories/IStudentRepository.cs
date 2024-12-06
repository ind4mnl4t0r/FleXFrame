using FleXFrame.AcademicSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Interfaces.IRepositories
{
    public interface IStudentRepository
    {
        Task<Student?> GetStudentByIDAsync(string studentID);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetLastAddedStudentAsync();
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(string studentID);
    }
}
