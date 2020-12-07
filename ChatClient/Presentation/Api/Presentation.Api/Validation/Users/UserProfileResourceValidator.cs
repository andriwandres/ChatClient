using Core.Domain.Resources.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class UserProfileResourceValidator : AbstractValidator<UserProfileResource>
    {
        public UserProfileResourceValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(model => model.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches(new Regex(@"\w*"));

            RuleFor(model => model.AvailabilityStatusId)
                .NotEmpty()
                .IsInEnum();
        }
    }
}
