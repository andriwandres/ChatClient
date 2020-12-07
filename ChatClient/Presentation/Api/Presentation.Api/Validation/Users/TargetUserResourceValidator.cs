using Core.Domain.Resources.Users;
using FluentValidation;

namespace Presentation.Api.Validation.Users
{
    public class TargetUserResourceValidator : AbstractValidator<TargetUserResource>
    {
        public TargetUserResourceValidator()
        {
            RuleFor(user => user.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(user => user.UserName)
                .NotEmpty();
        }
    }
}
