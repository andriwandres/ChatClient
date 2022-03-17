using Core.Domain.ViewModels.Groups;
using FluentValidation;

namespace Presentation.Api.Validation.Groups;

public class GroupResourceValidator : AbstractValidator<GroupViewModel>
{
    public GroupResourceValidator()
    {
        RuleFor(group => group.GroupId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(group => group.Name)
            .NotEmpty();

        RuleFor(group => group.Description);

        RuleFor(group => group.Created)
            .NotEmpty();
    }
}