namespace Core.Domain.Dtos.Session
{
    public class LoginCredentialsDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
