using FleXFrame.AcademicSuite.Interfaces.IRepositories;
using FleXFrame.AcademicSuite.Models;
using Microsoft.EntityFrameworkCore;

namespace FleXFrame.AcademicSuite.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        // Define the DateContext
        private readonly AcademicSuiteDataContext _context;

        public StudentRepository(AcademicSuiteDataContext context)
        {
            // Initializing the DataContext
            _context = context;
        }

        // Get single Student using StudentID
        public async Task<Student?> GetStudentByIDAsync(string studentID)
        {
            return await _context.Students.FindAsync(studentID);
        }

        // Get all Students
        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student?> GetLastAddedStudentAsync()
        {
            return await _context.Students.OrderByDescending(s => s.StudentID).FirstOrDefaultAsync();
        }

        // Add a new Student
        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        // Update existing Student by StudentID
        public async Task UpdateStudentAsync(Student student)
        {
            _context.Students.Update(student);
            await _context.SaveChangesAsync();
        }

        // Delete existing Student by StudentID
        public async Task DeleteStudentAsync(string studentID)
        {
            var student = await GetStudentByIDAsync(studentID);
            if (student != null)
            {
                _context.Remove(student);
                await _context.SaveChangesAsync();
            }
        }
    }
}
