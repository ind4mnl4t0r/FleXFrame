using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.Core.Models
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

        [Required]
        [MaxLength(20)]
        public required string PermissionID { get; set; }

        [Required]
        [MaxLength(250)]
        public required string PermissionName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }


        public PermissionStatuses PermissionStatus { get; set; }

        public PermissionTypes PermissionType { get; set; }

        [MaxLength(300)]
        public string? Module { get; set; }


        public AccessLevels AccessLevel { get; set; }

        public int PriorityLevel { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(20)]
        public required string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? ModifiedBy { get; set; }


        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
