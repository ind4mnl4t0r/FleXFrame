using AutoMapper;
using FleXFrame.AuthHub.DTOs.RoleDtos;
using FleXFrame.AuthHub.Models;
using FleXFrame.UtilityHub;
using FleXFrame.UtilityHub.ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Services
{
    public class RoleService
    {
        private readonly AuthHubDataContext _context;
        private readonly IMapper _mapper;

        public RoleService(AuthHubDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateRoleAsync(RoleCreateDto roleCreateDto)
        {
            string newRoleID = roleCreateDto.RoleID ?? GenerateDefaultRoleID();

            var newRole = _mapper.Map<Role>(roleCreateDto);
            newRole.RoleID = newRoleID;
            newRole.DateCreated = DateTime.Now;

            await _context.Roles.AddAsync(newRole);
            await _context.SaveChangesAsync();

            return newRoleID;
        }

        public async Task<Result<RoleViewDto>> GetRoleByIdAsync(string roleID)
        {
            var user = await _context.Roles.FindAsync(roleID);

            if (user == null)
            {
                // Return a failure result with an error message
                return Result<RoleViewDto>.Failure($"Role with ID '{roleID}' not found");
            }

            // Return a success result with the mapped UserViewDto data
            return Result<RoleViewDto>.Success(_mapper.Map<RoleViewDto>(user));
        }

        // Default UserID
        private string GenerateDefaultRoleID()
        {
            // Retrieve the latest user to determine the current sequence number
            var latestUser = _context.Roles.OrderByDescending(u => u.RoleID).FirstOrDefault();

            int sequenceNumber = 1; // Default sequence if no users exist yet

            if (latestUser?.RoleID != null)
            {
                // Parse the sequence part of the ID from the latest user
                var sequencePart = latestUser.RoleID.Substring(latestUser.RoleID.Length - 4);
                if (int.TryParse(sequencePart, out int latestSequence))
                {
                    sequenceNumber = latestSequence + 1; // Increment the sequence number
                }
            }

            // Generate a new unique ID using the provided pattern
            return IDGenerator.GenerateID("ROLE-{S}", sequenceNumber);
        }
    }
}
