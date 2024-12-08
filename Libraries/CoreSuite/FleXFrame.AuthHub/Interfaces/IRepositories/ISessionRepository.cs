using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Interfaces.IRepositories
{
    public interface ISessionRepository
    {
        Task<Result<Session>> CreateSessionAsync(Session session);
        Task<Result<Session>> GetSessionByTokenAsync(string token);
        Task<Result<List<Session>>> GetSessionsByUserAsync(string userId);
        Task<Result<bool>> UpdateSessionStatusAsync(string token, Session.SessionStatuses status);
        Task<Result<bool>> DeleteSessionAsync(string token);
        Task<Result<List<Session>>> GetExpiredSessionsAsync();
        Task<Result<bool>> RevokeSessionAsync(string token);
        Task<Result<bool>> IsTokenRevokedAsync(string token);
    }
}
