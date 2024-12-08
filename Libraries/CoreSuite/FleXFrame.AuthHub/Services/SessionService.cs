using FleXFrame.AuthHub.Helpers;
using FleXFrame.AuthHub.Interfaces.IRepositories;
using FleXFrame.AuthHub.Interfaces.IServices;
using FleXFrame.AuthHub.Models;
using FleXFrame.AuthHub.Repositories;
using FleXFrame.UtilityHub.ErrorHandling;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repository;

        public SessionService(ISessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<string>> StartSessionAsync(Session session, JwtHelper jwtHelper)
        {
            // Validate session details
            session.ExpiryTime = session.ExpiryTime.ToUniversalTime(); // Ensure UTC
            session.SessionStatus = Session.SessionStatuses.Active;

            // Save session to database
            var result = await _repository.CreateSessionAsync(session);

            return result.IsSuccess
                ? Result<string>.Success(session.Token) // Return the session token
                : Result<string>.Failure(result.Error ?? "Failed to create session.");
        }


        // Revoke a session
        public async Task<Result<bool>> RevokeSessionAsync(string token)
        {
            return await _repository.RevokeSessionAsync(token);
        }

        // Validate a token and check its status
        public async Task<Result<bool>> ValidateTokenAsync(string token, JwtHelper jwtHelper)
        {
            // Validate JWT
            if (!jwtHelper.ValidateToken(token))
                return Result<bool>.Failure("Invalid token.");

            // Check session status
            var isRevoked = await _repository.IsTokenRevokedAsync(token);
            return isRevoked.Data == false
                ? Result<bool>.Success(true)
                : Result<bool>.Failure("Token has been revoked.");
        }

        public async Task<Result<Session>> GetSessionByTokenAsync(string token)
        {
            var sessionResult = await _repository.GetSessionByTokenAsync(token);
            if (!sessionResult.IsSuccess)
                return sessionResult;

            var session = sessionResult.Data!;
            if (session.ExpiryTime <= DateTime.UtcNow)
            {
                await _repository.UpdateSessionStatusAsync(token, Session.SessionStatuses.Inactive);
                return Result<Session>.Failure("Session has expired.");
            }

            return sessionResult;
        }

        public async Task<Result<List<Session>>> GetSessionsByUserAsync(string userId)
        {
            return await _repository.GetSessionsByUserAsync(userId);
        }

        public async Task<Result<bool>> EndSessionAsync(string token)
        {
            return await _repository.UpdateSessionStatusAsync(token, Session.SessionStatuses.Inactive);
        }

        public async Task<Result<int>> CleanUpExpiredSessionsAsync()
        {
            // Fetch expired sessions
            var expiredSessionsResult = await _repository.GetExpiredSessionsAsync();

            // Check if the operation succeeded
            if (!expiredSessionsResult.IsSuccess)
                return Result<int>.Failure(expiredSessionsResult.Error ?? "Failed to fetch expired sessions.");

            // Ensure Data is not null
            var expiredSessions = expiredSessionsResult.Data;
            if (expiredSessions == null || !expiredSessions.Any())
            {
                return Result<int>.Success(0); // No expired sessions found
            }

            // Update the status of each expired session
            int updatedCount = 0;
            foreach (var session in expiredSessions)
            {
                var updateResult = await _repository.UpdateSessionStatusAsync(session.Token, Session.SessionStatuses.Inactive);
                if (updateResult.IsSuccess)
                {
                    updatedCount++;
                }
                else
                {
                    // Optionally log or handle update failures for individual sessions
                    Console.WriteLine($"Failed to update session {session.Token}: {updateResult.Error}");
                }
            }

            return Result<int>.Success(updatedCount);
        }

    }
}
