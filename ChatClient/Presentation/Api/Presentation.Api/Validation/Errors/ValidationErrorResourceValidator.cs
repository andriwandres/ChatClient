﻿using Core.Domain.ViewModels.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Presentation.Api.Validation.Errors;

public class ValidationErrorResourceValidator : AbstractValidator<ValidationErrorViewModel>
{
    public ValidationErrorResourceValidator()
    {
        RuleFor(error => error.StatusCode)
            .NotEmpty()
            .InclusiveBetween(StatusCodes.Status100Continue, StatusCodes.Status511NetworkAuthenticationRequired);

        RuleFor(error => error.Message)
            .NotEmpty();

        RuleFor(error => error.Errors)
            .NotEmpty();
    }
}