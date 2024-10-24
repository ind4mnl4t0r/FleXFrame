using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.PermissionManagement.Models
{
    public class UserRole
    {
        public required string UserRoleID { get; set; }
        public required string UserID { get; set; }
        public required string RoleID { get; set; }
        public DateTime DateAssigned { get; set; }
        public required string AssignedBy { get; set; }
        public bool IsPrimaryRole { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public required virtual User User { get; set; }
        public required virtual Role Role { get; set; } 
    }

}
