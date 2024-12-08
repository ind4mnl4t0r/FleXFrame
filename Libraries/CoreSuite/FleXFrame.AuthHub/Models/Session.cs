using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.Models
{
    public class Session
    {
        public enum SessionStatuses { Active, Inactive, Revoked, Deleted }

        [Required]
        [MaxLength(50)]
        public required string UserID { get; set; }

        [Required]
        public required string Token { get; set; }

        [Required]
        public required DateTime ExpiryTime { get; set; }

        public required SessionStatuses SessionStatus { get; set; }

        // Additional fields for advanced session management
        public string? IPAddress { get; set; }
        public string? DeviceInfo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property to User (optional, if needed for querying)
        public virtual User? User { get; set; }
    }

}
