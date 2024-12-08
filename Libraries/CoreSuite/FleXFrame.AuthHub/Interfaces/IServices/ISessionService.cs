using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IServices
{
    public interface ISessionService
    {
        Task<Result<string>> StartSessionAsync(Session session, JwtHelper jwtHelper);
        Task<Result<Session>> GetSessionByTokenAsync(string token);
        Task<Result<List<Session>>> GetSessionsByUserAsync(string userId);
        Task<Result<bool>> EndSessionAsync(string token);
    }
}
