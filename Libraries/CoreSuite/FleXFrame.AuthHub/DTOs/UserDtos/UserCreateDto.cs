namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserCreateDto
    {
        public string? UserID { get; set; }
        public required string Username { get; set; }
        public required string Name { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public required DateTime DateCreated { get; set; }
        public required string CreatedBy { get; set; }
    }
}
