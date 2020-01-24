using ChatClient.Core.Models.ViewModels;
using FluentValidation;

namespace ChatClient.Api.ViewModels.Validation
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
