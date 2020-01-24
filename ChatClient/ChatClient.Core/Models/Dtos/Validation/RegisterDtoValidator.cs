using FluentValidation;

namespace ChatClient.Core.Models.Dtos.Validation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(register => register.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(register => register.DisplayName)
                .NotEmpty();

            RuleFor(register => register.Password)
                .NotEmpty()
                .Matches(@"^\S+$");
        }
    }
}
