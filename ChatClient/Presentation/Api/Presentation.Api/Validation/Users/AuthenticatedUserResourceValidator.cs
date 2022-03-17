﻿using FluentValidation;
using System.Text.RegularExpressions;
using Core.Domain.ViewModels.Users;

namespace Presentation.Api.Validation.Users;

public class AuthenticatedUserResourceValidator : AbstractValidator<AuthenticatedUserViewModel>
{
    public AuthenticatedUserResourceValidator()
    {
        RuleFor(model => model.UserId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(model => model.Token)
            .NotEmpty()
            .Matches(new Regex(@"(?:\w+\.){2}\w+"));

        RuleFor(model => model.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(model => model.UserName)
            .NotEmpty()
            .MinimumLength(2)
            .Matches(new Regex(@"\w{2,}"));
    }
}