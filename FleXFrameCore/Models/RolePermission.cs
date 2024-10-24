using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.PermissionManagement.Models
{
    public class RolePermission
    {
        public required string RolePermissionID { get; set; }
        public required string RoleID { get; set; }
        public required string PermissionID { get; set; }
        public DateTime DateAssigned { get; set; }
        public required string AssignedBy { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public required virtual Role Role { get; set; } 
        public required virtual Permission Permission { get; set; }
    }

}
