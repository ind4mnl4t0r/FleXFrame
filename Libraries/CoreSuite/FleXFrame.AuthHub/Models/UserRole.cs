using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Models
{
    public class UserRole
    {
        [Key]
        [Required]
        [MaxLength(length: 50)]
        public required string UserRoleID { get; set; }

        [Required]
        [MaxLength(50)]
        public required string UserID { get; set; }
        public required virtual User User { get; set; }

        [Required]
        [MaxLength(50)]
        public required string RoleID { get; set; }
        public required virtual Role Role { get; set; }

        [Required]
        public required DateTime DateAssigned { get; set; }

        [Required]
        [MaxLength(50)]
        public required string AssignedBy { get; set; }

        public bool IsPrimaryRole { get; set; }

        public DateTime? ExpiryDate { get; set; }
    }

}
