using Core.Domain.Dtos.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class RegisterUserDtoValidator : AbstractValidator<CreateAccountBody>
    {
        public RegisterUserDtoValidator()
        {
            const string emailName = nameof(CreateAccountBody.Email);
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage($"'{emailName}' must not be empty")
                .EmailAddress()
                .WithMessage($"'{emailName}' has an invalid e-mail address format");

            const int userNameMinLength = 2;
            const string userNameName = nameof(CreateAccountBody.UserName);
            RuleFor(model => model.UserName)
                .NotEmpty()
                .WithMessage($"'{userNameName}' must not be empty")
                .MinimumLength(userNameMinLength)
                .WithMessage(actual => $"'{userNameName}' must be at least {userNameMinLength} characters long. You entered {actual.UserName.Length} characters")
                .Matches(new Regex(@"\w*"))
                .WithMessage($"'{userNameName}' contains illegal characters. Use only alphanumeric characters including underscores");

            const int passwordMinLength = 8;
            const string passwordName = nameof(CreateAccountBody.Password);
            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage($"'{passwordName}' must not be empty")
                .MinimumLength(8)
                .WithMessage(actual => $"'{passwordName}' must be at least {passwordMinLength} characters long. You entered {actual.UserName.Length} characters");
        }
    }
}
