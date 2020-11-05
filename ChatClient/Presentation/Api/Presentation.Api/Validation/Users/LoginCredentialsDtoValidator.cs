using Core.Domain.Dtos.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class LoginCredentialsDtoValidator : AbstractValidator<LoginCredentialsDto>
    {
        public LoginCredentialsDtoValidator()
        {
            RuleFor(model => model.UserNameOrEmail)
                .NotEmpty()
                .MinimumLength(2);

            When(model => model.UserNameOrEmail.Contains('@'), () =>
                RuleFor(model => model.UserNameOrEmail)
                    .EmailAddress())

            .Otherwise(() => 
                RuleFor(model => model.UserNameOrEmail)
                    .Matches(new Regex(@"\w{2,}")));

            RuleFor(model => model.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
