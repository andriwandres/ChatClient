using FluentValidation;

namespace ChatClient.Core.Models.Dtos.Auth.Validation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(login => login.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(login => login.Password)
                .NotEmpty()
                .Matches(@"^\S+$");
        }
    }
}
