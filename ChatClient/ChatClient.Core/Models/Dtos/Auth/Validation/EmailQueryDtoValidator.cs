using FluentValidation;

namespace ChatClient.Core.Models.Dtos.Auth.Validation
{
    public class EmailQueryDtoValidator : AbstractValidator<EmailQueryDto>
    {
        public EmailQueryDtoValidator()
        {
            RuleFor(query => query.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
