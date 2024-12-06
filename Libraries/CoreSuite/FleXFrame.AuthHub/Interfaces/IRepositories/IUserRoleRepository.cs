using FleXFrame.AuthHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IRepositories
{
    public interface IUserRoleRepository
    {
        Task AddUserRoleAsync(string userID, string roleID);
        Task<IEnumerable<Role>> GetRolesForUserAsync(string userID);
        Task RemoveUserRoleAsync(string userID, string roleID);
    }
}
