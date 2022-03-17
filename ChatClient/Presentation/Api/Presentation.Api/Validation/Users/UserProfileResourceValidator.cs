﻿using FluentValidation;
using System.Text.RegularExpressions;
using Core.Domain.ViewModels.Users;

namespace Presentation.Api.Validation.Users;

public class UserProfileResourceValidator : AbstractValidator<UserProfileViewModel>
{
    public UserProfileResourceValidator()
    {
        RuleFor(model => model.UserId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(model => model.UserName)
            .NotEmpty()
            .MinimumLength(2)
            .Matches(new Regex(@"\w*"));

        RuleFor(model => model.AvailabilityStatus)
            .NotEmpty()
            .IsInEnum();
    }
}