using Core.Domain.Dtos.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages;

public class GetMessagesWithRecipientQueryParamsValidator : AbstractValidator<GetMessagesWithRecipientQueryParams>
{
    public GetMessagesWithRecipientQueryParamsValidator()
    {
        RuleFor(model => model.Limit)
            .GreaterThan(0)
            .WithMessage("Limit must be greater than zero");

        When(model => model.Before != null && model.After != null, () =>
        {
            RuleFor(model => model.Before)
                .GreaterThan(model => model.After)
                .WithMessage("'Before' must be at a later point in time than 'After'");
        });
    }
}