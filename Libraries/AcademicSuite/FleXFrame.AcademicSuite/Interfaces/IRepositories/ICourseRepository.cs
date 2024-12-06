using FleXFrame.AcademicSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Interfaces.IRepositories
{
    public interface ICourseRepository
    {
        Task<Course?> GetCourseByIDAsync(string courseID);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<Student?> GetLastAddedBatchAsync();
        Task AddCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(string courseID);
    }
}
