using Core.Domain.Resources.Recipients;
using FluentValidation;

namespace Presentation.Api.Validation.Recipients
{
    public class RecipientResourceValidator : AbstractValidator<RecipientResource>
    {
        public RecipientResourceValidator()
        {
            RuleFor(recipient => recipient.RecipientId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(recipient => recipient.UnreadMessagesCount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(recipient => recipient.LatestMessage)
                .NotEmpty();
        }
    }
}
