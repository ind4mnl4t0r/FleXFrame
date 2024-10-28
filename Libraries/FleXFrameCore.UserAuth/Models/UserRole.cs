using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Models
{
    public class UserRole
    {
        [Required]
        [MaxLength(20)]
        public required string UserRoleID { get; set; }

        [Required]
        [MaxLength(20)]
        public required string UserID { get; set; }

        [Required]
        [MaxLength(20)]
        public required string RoleID { get; set; }

        public DateTime DateAssigned { get; set; }

        [Required]
        [MaxLength(20)]
        public required string AssignedBy { get; set; }

        public bool IsPrimaryRole { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public required virtual User User { get; set; }
        public required virtual Role Role { get; set; }
    }

}
