using Core.Domain.Dtos.GroupMemberships;
using FluentValidation;

namespace Presentation.Api.Validation.GroupMemberships
{
    public class CreateMembershipDtoValidator : AbstractValidator<CreateMembershipBody>
    {
        public CreateMembershipDtoValidator()
        {
            RuleFor(model => model.UserId)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(model => model.GroupId)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
