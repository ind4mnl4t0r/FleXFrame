using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Models
{
    public class RolePermission
    {
        [Required]
        [MaxLength(20)]
        public required string RolePermissionID { get; set; }

        [Required]
        [MaxLength(20)]
        public required string RoleID { get; set; }

        [Required]
        [MaxLength(20)]
        public required string PermissionID { get; set; }

        public DateTime DateAssigned { get; set; }

        [Required]
        [MaxLength(20)]
        public required string AssignedBy { get; set; }

        public bool IsRevoked { get; set; }

        public DateTime? ExpiryDate { get; set; }


        public required virtual Role Role { get; set; } 
        public required virtual Permission Permission { get; set; }
    }

}
