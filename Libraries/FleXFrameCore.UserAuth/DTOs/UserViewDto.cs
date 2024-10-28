using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FleXFrameCore.UserAuth.Models.User;

namespace FleXFrameCore.UserAuth.DTOs
{
    public class UserViewDto
    {
        public enum UserStatuses
        {
            Active,
            Inactive,
            Suspended,
            Deleted
        }

        public required string UserID { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public UserStatuses? UserStatus { get; set; }
    }
}
