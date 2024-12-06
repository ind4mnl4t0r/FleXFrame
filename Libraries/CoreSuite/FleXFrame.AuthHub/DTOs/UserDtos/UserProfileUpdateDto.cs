using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserProfileUpdateDto
    {
        public required string UserID { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? NationalIDNumber { get; set; }


        public required DateTime LastModified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
