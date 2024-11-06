using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrameCore.UserAuth.Models
{
    public class User
    {
        public enum UserStatuses
        {
            Active,
            Inactive,
            Suspended,
            Deleted
        }

        [MaxLength(20)]
        public string? UserID { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Username { get; set; }

        [Required]
        public byte[]? PasswordHash { get; set; }

        [Required]
        public byte[]? PasswordSalt { get; set; }

        [Required]
        [MaxLength(250)]
        public required string Name { get; set; }

        [MaxLength(150)]
        public string? Email { get; set; }

        [MaxLength(16)]
        public string? Phone { get; set; }


        public bool IsActive { get; set; }


        public UserStatuses? UserStatus { get; set; }


        public DateTime DateCreated { get; set; }

        [Required]
        [MaxLength(20)]
        public required string CreatedBy { get; set; }


        public DateTime? LastModified { get; set; }


        public string? ModifiedBy { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }

}
