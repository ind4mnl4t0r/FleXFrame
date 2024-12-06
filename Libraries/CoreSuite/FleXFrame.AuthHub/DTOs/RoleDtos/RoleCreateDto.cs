using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.RoleDtos
{
    public class RoleCreateDto
    {
        public required string RoleID { get; set; }
        public required string RoleName { get; set; }
        public required DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
    }
}
