using FleXFrame.AuthHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IRepositories
{
    public interface IRolePermissionRepository
    {
        Task AddPermissionToRoleAsync(string roleID, string permissionID);
        Task<IEnumerable<Permission>> GetPermissionsForRoleAsync(string roleID);
        Task RemovePermissionFromRoleAsync(string roleID, string permissionID);
    }
}
