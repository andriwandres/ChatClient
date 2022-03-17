﻿using Core.Domain.ViewModels.Friendships;
using FluentValidation;

namespace Presentation.Api.Validation.Friendships;

public class FriendshipResourceValidator : AbstractValidator<FriendshipViewModel>
{
    public FriendshipResourceValidator()
    {
        RuleFor(friendship => friendship.FriendshipId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(friendship => friendship.RequesterId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(friendship => friendship.AddresseeId)
            .NotEmpty()
            .GreaterThan(0);
    }
}