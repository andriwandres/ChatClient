using Core.Domain.Dtos.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class UserNameExistsDtoValidator : AbstractValidator<UserNameExistsDto>
    {
        public UserNameExistsDtoValidator()
        {
            RuleFor(model => model.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches(new Regex(@"\w{2,}"));
        }
    }
}
