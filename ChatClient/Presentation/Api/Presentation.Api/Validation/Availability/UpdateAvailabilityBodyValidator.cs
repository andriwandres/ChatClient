using Core.Domain.Dtos.Availability;
using Core.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Api.Validation.Availability;

public class UpdateAvailabilityBodyValidator : AbstractValidator<UpdateAvailabilityBody>
{
    public UpdateAvailabilityBodyValidator()
    {
        const string availabilityStatusName = nameof(UpdateAvailabilityBody.AvailabilityStatus);
        IEnumerable<int> values = Enum
            .GetValues(typeof(AvailabilityStatus))
            .Cast<int>();

        string valuesString = string.Join(", ", values);

        RuleFor(body => body.AvailabilityStatus)
            .NotEmpty()
            .WithMessage($"'{availabilityStatusName}' must not be empty")
            .IsInEnum()
            .WithMessage($"'{availabilityStatusName}' must be one of the following values: {valuesString}");
    }
}