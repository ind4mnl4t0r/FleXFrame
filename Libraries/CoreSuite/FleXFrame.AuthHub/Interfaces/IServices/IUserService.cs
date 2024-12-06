using FleXFrame.AuthHub.DTOs.UserDtos;
using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IServices
{
    public interface IUserService
    {
        Task<Result<string>> CreateUserAsync(UserCreateDto userCreateDto);
        Task<Result<bool>> ValidateUserAsync(string username, string password);
        Task<Result<UserViewDto>> GetUserByIdAsync(string userID);
        Task<User?> UpdateUserPasswordAsync(UserPasswordUpdateDto userPasswordUpdateDto);
        Task<User?> UpdateUserProfileAsync(UserProfileUpdateDto userProfileUpdateDto);
        Task<User?> UpdateUserStatusAsync(UserStatusUpdateDto userStatusUpdateDto);
        Task<string> GenerateUserIDAsync(string pattern = "USER-{S}");

    }
}
