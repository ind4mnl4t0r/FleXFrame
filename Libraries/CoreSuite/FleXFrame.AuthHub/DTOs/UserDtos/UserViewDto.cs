namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserViewDto
    {
        public enum UserStatuses { Active, Inactive, Suspended, Deleted }

        public required string UserID { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public UserStatuses? UserStatus { get; set; }
    }
}
