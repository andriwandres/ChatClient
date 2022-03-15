using Core.Domain.Resources.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Presentation.Api.Validation.Errors;

public class ErrorResourceValidator : AbstractValidator<ErrorResource>
{
    public ErrorResourceValidator()
    {
        RuleFor(error => error.StatusCode)
            .NotEmpty()
            .InclusiveBetween(StatusCodes.Status100Continue, StatusCodes.Status511NetworkAuthenticationRequired);

        RuleFor(error => error.Message)
            .NotEmpty();
    }
}