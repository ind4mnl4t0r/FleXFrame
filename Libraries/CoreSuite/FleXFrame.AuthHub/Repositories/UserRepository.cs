﻿using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthHubDataContext _context;

        public UserRepository(AuthHubDataContext dataContext)
        {
            _context = dataContext;
        }

        public async Task<User?> GetUserByIDAsync(string userID)
        {
            return await _context.Users.FindAsync(userID);
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> IsUsernameExistAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string userID)
        {
            var user = await GetUserByIDAsync(userID);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<User?> GetLastAddedUserAsync()
        {
            return await _context.Users.OrderByDescending(u => u.UserID).FirstOrDefaultAsync();
        }
    }
}