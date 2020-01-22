using FluentValidation;

namespace ChatClient.Core.Models.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.UserId)
                .NotNull();

            RuleFor(user => user.UserTag)
                .NotEmpty()
                .Length(6);

            RuleFor(user => user.DisplayName)
                .NotEmpty()
                .MinimumLength(2);

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(user => user.PasswordHash)
                .NotEmpty();

            RuleFor(user => user.PasswordSalt)
                .NotEmpty();

            RuleFor(user => user.CreatedAt)
                .NotEmpty();
        }
    }
}
