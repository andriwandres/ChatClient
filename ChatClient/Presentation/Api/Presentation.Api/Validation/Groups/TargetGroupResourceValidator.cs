using Core.Domain.Resources.Groups;
using FluentValidation;

namespace Presentation.Api.Validation.Groups
{
    public class TargetGroupResourceValidator : AbstractValidator<TargetGroupResource>
    {
        public TargetGroupResourceValidator()
        {
            RuleFor(group => group.GroupId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(group => group.Name)
                .NotEmpty();
        }
    }
}
