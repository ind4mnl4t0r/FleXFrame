namespace FleXFrameCore.UserManagement.Interfaces
{
    public interface IUser
    {
        public string UserID { get; }
        public string Username { get; }
        public bool IsActive { get; }
    }
}
