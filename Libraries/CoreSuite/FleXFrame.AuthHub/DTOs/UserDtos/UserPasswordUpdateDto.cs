using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserPasswordUpdateDto
    {
        public required string UserID { get; set; }
        public required string Username { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required DateTime LastModified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
