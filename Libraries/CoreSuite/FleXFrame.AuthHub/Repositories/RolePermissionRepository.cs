using FleXFrame.AuthHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Repositories
{
    public class RolePermissionRepository
    {
        private readonly AuthHubDataContext _context;

        public RolePermissionRepository(AuthHubDataContext context)
        {
            _context = context;
        }

        public async Task<RolePermission?> GetRolePermissionByIDAsync(string rolePermissionID)
        {
            return await _context.RolePermissions.FindAsync(rolePermissionID);
        }

        public async Task<IEnumerable<RolePermission>> GetAllRolePermissionsAsync()
        {
            return await _context.RolePermissions.ToListAsync();
        }

        public async Task AddRolePermissionAsync(RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRolePermissionAsync(RolePermission rolePermission)
        {
            _context.RolePermissions.Update(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermissionAsync(string rolePermissionID)
        {
            var rolePermission = await GetRolePermissionByIDAsync(rolePermissionID);
            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RolePermission?> GetLastAddedRolePermissionAsync()
        {
            return await _context.RolePermissions.OrderByDescending(u => u.RolePermissionID).FirstOrDefaultAsync();
        }
    }
}
