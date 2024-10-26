using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Models
{
    public class Role
    {
        public required string RoleID { get; set; }
        public required string RoleName { get; set; }
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsSystemRole { get; set; }
        public int PriorityLevel { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
