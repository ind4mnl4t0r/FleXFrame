using FleXFrame.AuthHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Repositories
{
    public class UserRoleRepository 
    {
        private readonly AuthHubDataContext _context;

        public UserRoleRepository(AuthHubDataContext context)
        {
            _context = context;
        }

        public async Task<UserRole?> GetUserRoleByIDAsync(string userRoleID)
        {
            return await _context.UserRoles.FindAsync(userRoleID);
        }

        public async Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
        {
            return await _context.UserRoles.ToListAsync();
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserRoleAsync(UserRole userRole)
        {
            _context.UserRoles.Update(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserRoleAsync(string userRoleID)
        {
            var userRole = await GetUserRoleByIDAsync(userRoleID);
            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserRole?> GetLastAddedUserRoleAsync()
        {
            return await _context.UserRoles.OrderByDescending(u => u.UserRoleID).FirstOrDefaultAsync();
        }
    }
}
