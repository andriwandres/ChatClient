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
                .NotEmpty()
                .WithMessage($"'{isAdminName}' must not be empty");
        }
    }
}
