namespace Core.Domain.Resources.Users
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
