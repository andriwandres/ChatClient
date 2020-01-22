using FluentValidation;

namespace ChatClient.Core.Models.Validation
{
    public class GroupMembershipValidator : AbstractValidator<GroupMembership>
    {
        public GroupMembershipValidator()
        {
            RuleFor(membership => membership.GroupMembershipId)
                .NotNull();

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
