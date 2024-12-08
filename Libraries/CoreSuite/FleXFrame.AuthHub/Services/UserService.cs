using AutoMapper;
using FleXFrame.AuthHub.DTOs.UserDtos;
using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Interfaces.IServices;
using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub;
using FleXFrame.UtilityHub.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<string>> CreateUserAsync(UserCreateDto userCreateDto)
        {
            try
            {
                // Check if the username already exists
                if (await _userRepository.IsUsernameExistAsync(userCreateDto.Username))
                    return Result<string>.Failure("The username is already taken. Please choose a different one.");

                // Generate or use the provided UserID
                string newUserID = userCreateDto.UserID ?? await GenerateUserIDAsync();

                // Map UserCreateDto to User entity
                var newUser = _mapper.Map<User>(userCreateDto);
                newUser.UserID = newUserID;
                newUser.DateCreated = DateTime.Now;

                // Save the new user to the database
                await _userRepository.AddUserAsync(newUser);

                // Return the generated UserID on success
                return Result<string>.Success(newUserID);
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and return a failure result
                return Result<string>.Failure($"An error occurred while creating the user: {ex.Message}");
            }
        }


        public async Task<Result<User>> ValidateUserAsync(string username, string password)
        {
            try
            {
                // Retrieve the user from the database
                var user = await _userRepository.GetUserByUsernameAsync(username);

                // If user is not found, return failure
                if (user == null)
                    return Result<User>.Failure("Invalid username or password.");

                var userValidationDto = _mapper.Map<UserValidationDto>(user);

                // Check if password salt or hash are null
                if (userValidationDto.PasswordSalt == null || userValidationDto.PasswordHash == null)
                {
                    return Result<User>.Failure("Invalid user credentials.");
                }

                // Hash the input password with the stored salt
                var hashedPassword = PasswordEngine.HashPassword(password, userValidationDto.PasswordSalt);

                // Compare the hashed password with the stored hash
                if (!hashedPassword.SequenceEqual(userValidationDto.PasswordHash))
                {
                    return Result<User>.Failure("Invalid username or password.");
                }

                // Return the user object if validation passes
                return Result<User>.Success(user);
            }
            catch (Exception ex)
            {
                // Log the exception (optional) and return a failure result
                return Result<User>.Failure($"An error occurred during validation: {ex.Message}");
            }
        }



        public async Task<Result<UserViewDto>> GetUserByIdAsync(string userID)
        {
            var user = await _userRepository.GetUserByIDAsync(userID);

            if (user == null)
            {
                // Return a failure result with an error message
                return Result<UserViewDto>.Failure($"User with ID '{userID}' not found");
            }

            // Return a success result with the mapped UserViewDto data
            return Result<UserViewDto>.Success(_mapper.Map<UserViewDto>(user));
        }



        public async Task<User?> UpdateUserPasswordAsync(UserPasswordUpdateDto userPasswordUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByIDAsync(userPasswordUpdateDto.UserID);

            if (existingUser == null)
                return null;

            _mapper.Map(userPasswordUpdateDto, existingUser);

            await _userRepository.UpdateUserAsync(existingUser);

            return existingUser;
        }

        public async Task<User?> UpdateUserProfileAsync(UserProfileUpdateDto userProfileUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByIDAsync(userProfileUpdateDto.UserID);

            if (existingUser == null)
                return null;

            _mapper.Map(userProfileUpdateDto, existingUser);

            await _userRepository.UpdateUserAsync(existingUser);

            return existingUser;
        }

        public async Task<User?> UpdateUserStatusAsync(UserStatusUpdateDto userStatusUpdateDto)
        {
            var existingUser = await _userRepository.GetUserByIDAsync(userStatusUpdateDto.UserID);

            if (existingUser == null)
                return null;

            _mapper.Map(userStatusUpdateDto, existingUser);

            await _userRepository.UpdateUserAsync(existingUser);

            return existingUser;
        }

        // Default UserID
        public async Task<string> GenerateUserIDAsync(string pattern = "USER-{S}")
        {
            // Retrieve the latest user to determine the current sequence number
            var latestUser = await _userRepository.GetLastAddedUserAsync();

            int sequenceNumber = 1; // Default sequence if no users exist yet

            if (latestUser != null)
            {
                // Parse the sequence part of the ID from the latest user
                var sequencePart = latestUser.UserID.Substring(latestUser.UserID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1; // Increment the sequence number
                }
            }

            // Generate a new unique ID using the provided pattern
            return IDGenerator.GenerateID(pattern, sequenceNumber);
        }

    }
}
