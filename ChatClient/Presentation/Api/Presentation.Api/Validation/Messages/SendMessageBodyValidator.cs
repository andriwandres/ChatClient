using Core.Domain.Dtos.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages
{
    public class SendMessageBodyValidator : AbstractValidator<SendMessageBody>
    {
        public SendMessageBodyValidator()
        {
            const string recipientIdName = nameof(SendMessageBody.RecipientId);
            const string parentIdName = nameof(SendMessageBody.ParentId);
            const string htmlContentName = nameof(SendMessageBody.HtmlContent);

            RuleFor(body => body.RecipientId)
                .NotEmpty()
                .WithMessage($"'{recipientIdName}' must not be empty")
                .GreaterThan(0)
                .WithMessage($"'{recipientIdName}' must be greater than 0");

            RuleFor(body => body.ParentId)
                .NotEmpty()
                .WithMessage($"'{parentIdName}' must not be empty")
                .GreaterThan(0)
                .WithMessage($"'{parentIdName}' must be greater than 0");

            RuleFor(body => body.HtmlContent)
                .NotEmpty()
                .WithMessage($"'{htmlContentName}' must not be empty");
        }
    }
}
