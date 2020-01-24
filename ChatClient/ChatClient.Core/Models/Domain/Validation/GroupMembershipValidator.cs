using FluentValidation;

namespace ChatClient.Core.Models.Domain.Validation
{
    public class GroupMembershipValidator : AbstractValidator<GroupMembership>
    {
        public GroupMembershipValidator()
        {
            RuleFor(membership => membership.UserId)
                .NotNull();

            RuleFor(membership => membership.GroupId)
                .NotNull();

            RuleFor(membership => membership.IsAdmin)
                .NotNull();

            RuleFor(membership => membership.CreatedAt)
                .NotEmpty();
        }
    }
}
