using Core.Domain.Dtos.Session;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Session
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            // UserNameOrEmail
            const int userNameOrEmailMinLength = 2;
            const string userNameOrEmailName = nameof(LoginUserDto.UserNameOrEmail);
            RuleFor(model => model.UserNameOrEmail)
                .NotEmpty()
                .WithMessage($"'{userNameOrEmailName}' must not be empty")
                .MinimumLength(userNameOrEmailMinLength)
                .WithMessage(actual => $"'{userNameOrEmailName}' must be at least {userNameOrEmailMinLength} characters long. You entered {actual.UserNameOrEmail.Length} characters");

            // UserNameOrEmail in case of E-Mail
            When(model => model.UserNameOrEmail.Contains('@'), () =>
                RuleFor(model => model.UserNameOrEmail)
                    .EmailAddress()
                    .WithMessage($"'{userNameOrEmailName}' must be a valid e-mail address, in case e-mail is the preffered login value")
            )

            // UserNameOrEmail in case of UserName
            .Otherwise(() =>
                RuleFor(model => model.UserNameOrEmail)
                    .Matches(new Regex(@"\w*"))
                    .WithMessage($"'{userNameOrEmailName}' contains illegal characters. Use only alphanumeric characters including underscores")
            );

            // Password
            const int passwordMinLength = 8;
            const string passwordName = nameof(LoginUserDto.Password);
            RuleFor(model => model.Password)
                .NotEmpty()
                .WithMessage($"'{passwordName}' must not be empty")
                .MinimumLength(passwordMinLength)
                .WithMessage(actual => $"'{passwordName}' must be at least {passwordMinLength} characters long. You entered {actual.Password.Length} characters");
        }
    }
}
