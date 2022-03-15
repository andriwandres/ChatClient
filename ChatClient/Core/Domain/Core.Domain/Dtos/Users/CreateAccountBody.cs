namespace Core.Domain.Dtos.Users;

public class CreateAccountBody
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}