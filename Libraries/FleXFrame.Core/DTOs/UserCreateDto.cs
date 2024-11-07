namespace FleXFrame.Core.DTOs
{
    public class UserCreateDto
    {
        public string? UserID { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public required byte[] PasswordHash { get; set; }
        public required byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public string? CreatedBy { get; set; }
    }
}
