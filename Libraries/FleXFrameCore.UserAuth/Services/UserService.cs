using AutoMapper;
using FleXCore.Utilities;
using FleXFrameCore.UserAuth.DTOs;
using FleXFrameCore.UserAuth.Models;
using FleXFrameCore.UtilityHub;
using FleXFrameCore.UtilityHub.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Services
{
    public class UserService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // Step 1: Retrieve the last user's ID (assuming IDs are sorted in ascending order)
            var lastUser = await _context.Users
                .OrderByDescending(u => u.UserID)
                .FirstOrDefaultAsync();

            int newSequenceNumber = 1; // Default sequence number if no users exist

            if (lastUser != null)
            {
                // Extract the numeric part of the last UserID
                var match = Regex.Match(lastUser.UserID, @"\d+$"); // Finds digits at the end of the ID
                if (match.Success)
                {
                    // Increment the extracted number by 1
                    newSequenceNumber = int.Parse(match.Value) + 1;
                }
            }

            // Step 2: Generate a new ID
            string newUserId = IDGenerator.GenerateID("USER-{S}", newSequenceNumber, 3);

            // Step 3: Map the UserCreateDto to a User entity and set the new ID
            var user = _mapper.Map<User>(userCreateDto);
            user.UserID = newUserId;

            // Step 4: Add the user to the context and save changes
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<Result<UserViewDto>> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                // Return a failure result with an error message
                return Result<UserViewDto>.Failure($"User with ID '{userId}' not found");
            }

            // Return a success result with the mapped UserViewDto data
            return Result<UserViewDto>.Success(_mapper.Map<UserViewDto>(user));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
