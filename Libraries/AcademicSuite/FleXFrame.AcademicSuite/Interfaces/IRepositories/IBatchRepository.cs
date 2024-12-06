using FleXFrame.AcademicSuite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AcademicSuite.Interfaces.IRepositories
{
    public interface IBatchRepository
    {
        Task<Batch?> GetBatchByIDAsync(string batchID);
        Task<IEnumerable<Batch>> GetAllBatchesAsync();
        Task<Student?> GetLastAddedBatchAsync();
        Task AddBatchAsync(Batch batch);
        Task UpdateBatchAsync(Batch batch);
        Task DeleteBatchAsync(string batchID);
    }
}
