using Core.Domain.Resources.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages
{
    public class MessageResourceValidator : AbstractValidator<MessageResource>
    {
        public MessageResourceValidator()
        {
            RuleFor(message => message.MessageId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(message => message.MessageRecipientId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(message => message.AuthorId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(message => message.ParentId)
                .GreaterThan(0);

            RuleFor(message => message.AuthorName)
                .NotEmpty();

            RuleFor(message => message.HtmlContent)
                .NotEmpty();

            RuleFor(message => message.Created)
                .NotEmpty();

            RuleFor(message => message.ReadDate);

            RuleFor(message => message.IsRead)
                .NotNull();

            RuleFor(message => message.IsEdited)
                .NotNull();

            RuleFor(message => message.IsOwnMessage)
                .NotNull();
        }
    }
}
