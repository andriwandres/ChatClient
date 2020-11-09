namespace Core.Domain.Dtos.Session
{
    public class LoginUserDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
