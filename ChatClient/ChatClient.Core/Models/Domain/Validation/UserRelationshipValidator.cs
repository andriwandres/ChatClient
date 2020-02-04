using FluentValidation;

namespace ChatClient.Core.Models.Domain.Validation
{
    public class UserRelationshipValidator : AbstractValidator<UserRelationship>
    {
        public UserRelationshipValidator()
        {
            RuleFor(ur => ur.UserRelationshipId)
                .NotEmpty();

            RuleFor(ur => ur.Initiator)
                .NotEmpty();

            RuleFor(ur => ur.TargetId)
                .NotEmpty();

            RuleFor(ur => ur.CreatedAt)
                .NotEmpty();

            RuleFor(ur => ur.Status)
                .NotEmpty();
        }
    }
}
