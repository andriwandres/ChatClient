using Core.Domain.Dtos.GroupMemberships;
using FluentValidation;

namespace Presentation.Api.Validation.GroupMemberships;

public class CreateMembershipBodyValidator : AbstractValidator<CreateMembershipBody>
{
    public CreateMembershipBodyValidator()
    {
        const string userIdName = nameof(CreateMembershipBody.UserId);
        const string groupIdName = nameof(CreateMembershipBody.GroupId);

        RuleFor(model => model.UserId)
            .NotEmpty()
            .WithMessage($"'{userIdName}' must not be empty")
            .GreaterThan(0)
            .WithMessage($"'{userIdName}' must be greater than 0");

        RuleFor(model => model.GroupId)
            .NotEmpty()
            .WithMessage($"'{groupIdName}' must not be empty")
            .GreaterThan(0)
            .WithMessage($"'{groupIdName}' must be greater than 0");
    }
}