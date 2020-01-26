using ChatClient.Core.Models.ViewModels.Auth;
using FluentValidation;

namespace ChatClient.Core.Models.ViewModels.Auth.Validation
{
    public class AuthenticatedUserValidator : AbstractValidator<AuthenticatedUser>
    {
        public AuthenticatedUserValidator()
        {
            RuleFor(user => user.UserId)
                .NotNull();

            RuleFor(user => user.UserCode)
                .NotEmpty()
                .Length(6);

            RuleFor(user => user.DisplayName)
                .NotEmpty();

            RuleFor(user => user.Token)
                .NotEmpty();

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
