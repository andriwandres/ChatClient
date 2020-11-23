﻿using Core.Domain.Dtos.Users;
using FluentValidation;

namespace Presentation.Api.Validation.Users
{
    public class EmailExistsDtoValidator : AbstractValidator<EmailExistsQueryParams>
    {
        public EmailExistsDtoValidator()
        {
            const string emailName = nameof(EmailExistsQueryParams.Email);
            RuleFor(model => model.Email)
                .NotEmpty()
                .WithMessage($"'{emailName}' must not be empty")
                .EmailAddress()
                .WithMessage($"'{emailName}' must be a valid e-mail address");
        }
    }
}
