using Core.Domain.Dtos.GroupMemberships;
using FluentValidation;

namespace Presentation.Api.Validation.GroupMemberships
{
    public class UpdateMembershipBodyValidator : AbstractValidator<UpdateMembershipBody>
    {
        public UpdateMembershipBodyValidator()
        {
            const string isAdminName = nameof(UpdateMembershipBody.IsAdmin);

            RuleFor(body => body.IsAdmin)
                .NotNull()
                .WithMessage($"'{isAdminName}' must not be null");
        }
    }
}
