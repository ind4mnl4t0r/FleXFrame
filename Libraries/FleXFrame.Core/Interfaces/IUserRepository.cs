using FleXFrame.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIDAsync(string userID);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userID);
    }
}
