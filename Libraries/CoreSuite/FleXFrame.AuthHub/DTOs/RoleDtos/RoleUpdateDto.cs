using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.RoleDtos
{
    public class RoleUpdateDto
    {
        public string? RoleName { get; set; }
        public string? Description { get; set; }
        public required DateTime LastModified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
