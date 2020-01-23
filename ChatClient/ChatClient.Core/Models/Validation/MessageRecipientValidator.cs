using FluentValidation;

namespace ChatClient.Core.Models.Validation
{
    public class MessageRecipientValidator : AbstractValidator<MessageRecipient>
    {
        public MessageRecipientValidator()
        {
            RuleFor(recipient => recipient.MessageId)
                .NotNull();

            RuleFor(recipient => recipient.IsRead)
                .NotNull();
        }
    }
}
