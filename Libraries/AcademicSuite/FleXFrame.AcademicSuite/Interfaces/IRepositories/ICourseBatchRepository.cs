using FleXFrame.AcademicSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Interfaces.IRepositories
{
    public interface ICourseBatchRepository
    {
        Task<CourseBatch?> GetCourseBatchByIDAsync(string courseID);
        Task<IEnumerable<CourseBatch>> GetAllCourseBatchesAsync();
        Task<Student?> GetLastAddedCourseBatchAsync();
        Task AddCourseBatchAsync(CourseBatch courseBatch);
        Task UpdateCourseBatchAsync(CourseBatch courseBatch);
        Task DeleteCourseBatchAsync(string courseBatchID);
    }
}
