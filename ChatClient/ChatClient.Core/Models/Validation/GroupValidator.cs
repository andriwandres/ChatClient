using FluentValidation;

namespace ChatClient.Core.Models.Validation
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(group => group.Name)
                .NotEmpty();

            RuleFor(group => group.CreatedAt)
                .NotEmpty();
        }
    }
}
