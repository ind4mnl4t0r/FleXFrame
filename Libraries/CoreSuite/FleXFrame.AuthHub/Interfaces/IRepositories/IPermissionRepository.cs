using FleXFrame.AuthHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IRepositories
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetPermissionByIDAsync(string permissionID);
        Task<IEnumerable<Permission>> GetAllPermissionsAsync();
        Task AddPermissionAsync(Permission permission);
        Task UpdatePermissionAsync(Permission permission);
        Task DeletePermissionAsync(string permissionID);
    }
}
