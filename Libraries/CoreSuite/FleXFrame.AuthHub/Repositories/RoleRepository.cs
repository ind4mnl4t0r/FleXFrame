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
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthHubDataContext _context;

        public RoleRepository(AuthHubDataContext context)
        {
            _context = context;
        }


        public async Task<Role?> GetRoleByIDAsync(string roleID)
        {
            return await _context.Roles.FindAsync(roleID);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(string roleID)
        {
            var role = await GetRoleByIDAsync(roleID);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Role?> GetLastAddedRoleAsync()
        {
            return await _context.Roles.OrderByDescending(u => u.RoleID).FirstOrDefaultAsync();
        }

    }
}
