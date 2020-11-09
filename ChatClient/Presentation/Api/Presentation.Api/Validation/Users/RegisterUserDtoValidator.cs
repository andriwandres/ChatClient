using Core.Domain.Dtos.Users;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Presentation.Api.Validation.Users
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(model => model.UserName)
                .NotEmpty()
                .MinimumLength(2)
                .Matches(new Regex(@"\w{2,}"));

            RuleFor(model => model.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
