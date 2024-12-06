using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Models
{
    public class Role
    {
        [Required]
        [MaxLength(50)]
        public required string RoleID { get; set; }

        [Required]
        [MaxLength(250)]
        public required string RoleName { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Audit Fields
        [Required]
        public required DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(50)]
        public required string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? ModifiedBy { get; set; }


        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }

}
