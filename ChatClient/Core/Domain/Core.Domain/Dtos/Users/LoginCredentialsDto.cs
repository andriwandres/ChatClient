namespace Core.Domain.Dtos.Users
{
    public class LoginCredentialsDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
