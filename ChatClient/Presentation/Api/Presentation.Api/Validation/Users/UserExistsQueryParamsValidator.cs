using Core.Domain.Dtos.Users;
using FluentValidation;

namespace Presentation.Api.Validation.Users
{
    public class UserExistsQueryParamsValidator : AbstractValidator<UserExistsQueryParams>
    {
        public UserExistsQueryParamsValidator()
        {
            RuleFor(m => m.UserName)
                .NotEmpty()
                .When(m => m.Email == null);

            RuleFor(m => m.Email)
                .NotEmpty()
                .When(m => m.UserName == null);
        }
    }
}
