using Core.Domain.Dtos.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages
{
    public class EditMessageBodyValidator : AbstractValidator<EditMessageBody>
    {
        public EditMessageBodyValidator()
        {
            const string htmlContentName = nameof(EditMessageBody.HtmlContent);

            RuleFor(body => body.HtmlContent)
                .NotEmpty()
                .WithMessage($"'{htmlContentName}' must not be empty");
        }
    }
}
