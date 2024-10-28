using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.DTOs
{
    public class UserCreateDto
    {
        public string UserID { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
    }
}
