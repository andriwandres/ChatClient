using Core.Domain.Resources.Messages;
using FluentValidation;

namespace Presentation.Api.Validation.Messages;

public class LatestMessageResourceValidator : AbstractValidator<LatestMessageResource>
{
    public LatestMessageResourceValidator()
    {
        RuleFor(message => message.MessageRecipientId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(message => message.MessageId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(message => message.AuthorId)
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