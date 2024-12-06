using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserValidationDto
    {
        public required string Username { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required byte[] PasswordHash { get; set; }
    }
}
