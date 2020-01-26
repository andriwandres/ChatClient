namespace ChatClient.Core.Models.ViewModels.Auth
{
    public class AuthenticatedUser
    {
        public int UserId { get; set; }
        public string UserCode { get; set; }
        public string Token { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
