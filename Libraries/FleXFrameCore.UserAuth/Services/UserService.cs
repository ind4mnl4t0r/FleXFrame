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

        public async Task<string> CreateUserAsync(UserCreateDto userCreateDto)
        {
            // If the user has provided a custom UserID, use it.
            string newUserID = userCreateDto.UserID ?? GenerateDefaultUserID();

            // Map UserCreateDto to User entity
            var newUser = _mapper.Map<User>(userCreateDto);
            newUser.UserID = newUserID;
            newUser.DateCreated = DateTime.Now;

            // Save the new user to the database
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return newUserID;  // Return the UserID back to the caller
        }

        public async Task<User?> GetLatestUserAsync()
        {
            return await _context.Users
                .OrderByDescending(u => u.UserID)
                .FirstOrDefaultAsync();
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

        // Default UserID
        private string GenerateDefaultUserID()
        {
            // Retrieve the latest user to determine the current sequence number
            var latestUser = _context.Users.OrderByDescending(u => u.UserID).FirstOrDefault();

            int sequenceNumber = 1; // Default sequence if no users exist yet

            if (latestUser?.UserID != null)
            {
                // Parse the sequence part of the ID from the latest user
                var sequencePart = latestUser.UserID.Substring(latestUser.UserID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1; // Increment the sequence number
                }
            }

            // Generate a new unique ID using the provided pattern
            return IDGenerator.GenerateID("USER-{S}", sequenceNumber);
        }
    }
}
