﻿using Core.Domain.ViewModels.GroupMemberships;
using FluentValidation;

namespace Presentation.Api.Validation.GroupMemberships;

public class GroupMembershipResourceValidator : AbstractValidator<GroupMembershipViewModel>
{
    public GroupMembershipResourceValidator()
    {
        RuleFor(membership => membership.GroupMembershipId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(membership => membership.GroupId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(membership => membership.UserId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(membership => membership.UserName)
            .NotEmpty()
            .MinimumLength(2);

        RuleFor(membership => membership.IsAdmin)
            .NotEmpty();
    }
}