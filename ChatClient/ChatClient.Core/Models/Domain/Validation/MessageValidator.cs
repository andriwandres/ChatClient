using FluentValidation;

namespace ChatClient.Core.Models.Domain.Validation
{
    public class MessageValidator : AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(message => message.AuthorId)
                .NotNull();

            RuleFor(message => message.TextContent)
                .NotEmpty();

            RuleFor(message => message.IsForwarded)
                .NotNull();

            RuleFor(message => message.IsEdited)
                .NotNull();

            RuleFor(message => message.CreatedAt)
                .NotEmpty();
        }
    }
}
