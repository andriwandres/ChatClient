using Core.Domain.Dtos.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class UserNameExistsQueryParamsValidator : AbstractValidator<UserNameExistsQueryParams>
    {
        public UserNameExistsQueryParamsValidator()
        {
            const int userNameMinLength = 2;
            const string userNameName = nameof(UserNameExistsQueryParams.UserName);
            RuleFor(model => model.UserName)
                .NotEmpty()
                .WithMessage($"'{userNameName}' must not be empty")
                .MinimumLength(2)
                .WithMessage(actual => $"'{userNameName}' must be at least {userNameMinLength} characters long. You entered {actual.UserName.Length} characters")
                .Matches(new Regex(@"\w*"))
                .WithMessage($"'{userNameName}' contains illegal characters. Use only alphanumeric characters including underscores");
        }
    }
}
