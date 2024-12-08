using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Models
{
    public class User
    {
        // Login Information
        public enum UserStatuses { Active, Inactive, Suspended, Deleted }

        [Required]
        [MaxLength(50)]
        public required string UserID { get; set; }

        [Required]
        [MaxLength(200)]
        public required string Username { get; set; }

        [Required]
        public byte[]? PasswordHash { get; set; }

        [Required]
        public byte[]? PasswordSalt { get; set; }


        // Personal Information
        [Required]
        [MaxLength(254)]
        public required string Name { get; set; }

        [MaxLength(254)]
        public string? Email { get; set; }

        [MaxLength(16)]
        public string? Phone { get; set; }
        [MaxLength(20)]
        public string? NationalIDNumber { get; set; }


        // User Status
        public bool IsActive { get; set; }

        public UserStatuses? UserStatus { get; set; }


        // Audit Fields
        [Required]
        public required DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(50)]
        public required string CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        [MaxLength(50)]
        public string? ModifiedBy { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<Session> Sessions { get; set; } = new List<Session>();
    }

}
