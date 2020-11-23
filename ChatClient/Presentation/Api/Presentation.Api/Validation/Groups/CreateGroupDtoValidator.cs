using Core.Domain.Dtos.Groups;
using FluentValidation;

namespace Presentation.Api.Validation.Groups
{
    public class CreateGroupDtoValidator : AbstractValidator<CreateGroupBody>
    {
        public CreateGroupDtoValidator()
        {
            const int nameMinLength = 2;
            const string nameName = nameof(CreateGroupBody.Name);

            RuleFor(model => model.Name)
                .NotEmpty()
                .WithMessage($"'{nameName}' must not be empty")
                .MinimumLength(nameMinLength)
                .WithMessage(actual => $"'{nameName}' must be at least {nameMinLength} characters long. You entered {actual.Name.Length} characters");

            RuleFor(model => model.Description);
        }
    }
}
