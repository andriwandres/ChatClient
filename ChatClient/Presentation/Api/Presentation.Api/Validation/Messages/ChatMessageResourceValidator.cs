using Core.Domain.Resources.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages
{
    public class ChatMessageResourceValidator : AbstractValidator<ChatMessageResource>
    {
        public ChatMessageResourceValidator()
        {
            RuleFor(message => message.MessageRecipientId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(message => message.MessageId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(message => message.AuthorName)
                .NotEmpty();

            RuleFor(message => message.HtmlContent)
                .NotEmpty();

            RuleFor(message => message.Created)
                .NotEmpty();

            RuleFor(message => message.IsRead)
                .NotNull();

            RuleFor(message => message.IsOwnMessage)
                .NotNull();
        }
    }
}
