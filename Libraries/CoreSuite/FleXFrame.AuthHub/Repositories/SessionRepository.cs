using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub.ErrorHandling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AuthHubDataContext _context;

        public SessionRepository(AuthHubDataContext context)
        {
            _context = context;
        }

        public async Task<Result<Session>> CreateSessionAsync(Session session)
        {
            try
            {
                _context.Sessions.Add(session);
                await _context.SaveChangesAsync();
                return Result<Session>.Success(session);
            }
            catch (Exception ex)
            {
                return Result<Session>.Failure($"Error creating session: {ex.Message}");
            }
        }

        public async Task<Result<Session>> GetSessionByTokenAsync(string token)
        {
            try
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
                return session != null
                    ? Result<Session>.Success(session)
                    : Result<Session>.Failure("Session not found.");
            }
            catch (Exception ex)
            {
                return Result<Session>.Failure($"Error retrieving session: {ex.Message}");
            }
        }

        // Mark token as revoked
        public async Task<Result<bool>> RevokeSessionAsync(string token)
        {
            try
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
                if (session == null)
                    return Result<bool>.Failure("Session not found.");

                session.SessionStatus = Session.SessionStatuses.Revoked;
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error revoking session: {ex.Message}");
            }
        }

        // Check if token is revoked or inactive
        public async Task<Result<bool>> IsTokenRevokedAsync(string token)
        {
            try
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
                return Result<bool>.Success(session != null && session.SessionStatus == Session.SessionStatuses.Revoked);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error checking token status: {ex.Message}");
            }
        }

        public async Task<Result<List<Session>>> GetSessionsByUserAsync(string userId)
        {
            try
            {
                var sessions = await _context.Sessions
                    .Where(s => s.UserID == userId)
                    .ToListAsync();
                return Result<List<Session>>.Success(sessions);
            }
            catch (Exception ex)
            {
                return Result<List<Session>>.Failure($"Error retrieving sessions: {ex.Message}");
            }
        }

        public async Task<Result<List<Session>>> GetExpiredSessionsAsync()
        {
            try
            {
                var expiredSessions = await _context.Sessions
                    .Where(s => s.ExpiryTime <= DateTime.UtcNow && s.SessionStatus == Session.SessionStatuses.Active)
                    .ToListAsync();
                return Result<List<Session>>.Success(expiredSessions);
            }
            catch (Exception ex)
            {
                return Result<List<Session>>.Failure($"Error retrieving expired sessions: {ex.Message}");
            }
        }

        public async Task<Result<bool>> UpdateSessionStatusAsync(string token, Session.SessionStatuses status)
        {
            try
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
                if (session == null)
                    return Result<bool>.Failure("Session not found.");

                session.SessionStatus = status;
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error updating session: {ex.Message}");
            }
        }

        public async Task<Result<bool>> DeleteSessionAsync(string token)
        {
            try
            {
                var session = await _context.Sessions.FirstOrDefaultAsync(s => s.Token == token);
                if (session == null)
                    return Result<bool>.Failure("Session not found.");

                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Error deleting session: {ex.Message}");
            }
        }
    }
}
