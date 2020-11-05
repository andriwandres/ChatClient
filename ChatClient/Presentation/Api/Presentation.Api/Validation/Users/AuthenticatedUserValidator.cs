using Core.Domain.Resources.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class AuthenticatedUserValidator : AbstractValidator<AuthenticatedUser>
    {
        public AuthenticatedUserValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(model => model.Token)
                .NotEmpty()
                .Matches(new Regex(@"(?:\w+\.){2}\w+"));

            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(model => model.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches(new Regex(@"\w{2,}"));
        }
    }
}
