using FleXFrameCore.UserManagement.Interfaces;

namespace FleXFrameCore.UserManagement.Models
{
    public class User : IUser
    {
        public enum UserStatuses
        {
            Active,
            Inactive,
            Suspended,
            Deleted
        }

        public required string UserID { get; set; }
        public required string Username { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public UserStatuses? UserStatus { get; set; }
        public DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? ModifiedBy { get; set; }
    }

}
