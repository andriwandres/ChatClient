namespace Core.Domain.Dtos.Session;

public class LoginBody
{
    public string UserNameOrEmail { get; set; }
    public string Password { get; set; }
}