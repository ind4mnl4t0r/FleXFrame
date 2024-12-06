using FleXFrame.AuthHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIDAsync(string userID);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<bool> IsUsernameExistAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetLastAddedUserAsync();
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(string userID);
    }
}
