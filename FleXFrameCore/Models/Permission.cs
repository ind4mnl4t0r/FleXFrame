using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.PermissionManagement.Models
{
    public class Permission
    {
        public enum PermissionStatuses
        {
            Active,
            Inactive,
            Deleted
        }

        public enum PermissionTypes
        {
            View,
            Add,
            Edit,
            Delete
        }

        public enum AccessLevels
        {
            Module,
            Global
        }

        public required string PermissionID { get; set; }
        public required string PermissionName { get; set; }
        public string? Description { get; set; }
        public PermissionStatuses PermissionStatus { get; set; }
        public PermissionTypes PermissionType { get; set; }
        public string? Module { get; set; }
        public AccessLevels AccessLevel { get; set; }
        public int PriorityLevel { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? ModifiedBy { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
