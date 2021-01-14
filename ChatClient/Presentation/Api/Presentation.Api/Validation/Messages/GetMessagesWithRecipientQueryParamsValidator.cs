using Core.Domain.Dtos.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages
{
    public class GetMessagesWithRecipientQueryParamsValidator : AbstractValidator<GetMessagesWithRecipientQueryParams>
    {
        public GetMessagesWithRecipientQueryParamsValidator()
        {
            RuleFor(model => model.Limit)
                .NotEmpty()
                .WithMessage("The limit must not be empty")
                .GreaterThan(0)
                .WithMessage("The limit must be greater than zero");
        }
    }
}
