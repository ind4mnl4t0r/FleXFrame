using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthHubDataContext _context;

        public PermissionRepository(AuthHubDataContext context)
        {
            _context = context;
        }

        public async Task<Permission?> GetPermissionByIDAsync(string permissionID)
        {
            return await _context.Permissions.FindAsync(permissionID);
        }

        public async Task<IEnumerable<Permission>> GetAllPermissionsAsync()
        {
            return await _context.Permissions.ToListAsync();
        }

        public async Task AddPermissionAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePermissionAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(string permissionID)
        {
            var permission = await GetPermissionByIDAsync(permissionID);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Permission?> GetLastAddedPermissionAsync()
        {
            return await _context.Permissions.OrderByDescending(u => u.PermissionID).FirstOrDefaultAsync();
        }
    }
}
