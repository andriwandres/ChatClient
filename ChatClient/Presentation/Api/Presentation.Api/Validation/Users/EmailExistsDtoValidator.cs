using Core.Domain.Dtos.Users;
using FluentValidation;

namespace Presentation.Api.Validation.Users
{
    public class EmailExistsDtoValidator : AbstractValidator<EmailExistsDto>
    {
        public EmailExistsDtoValidator()
        {
            RuleFor(model => model.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
