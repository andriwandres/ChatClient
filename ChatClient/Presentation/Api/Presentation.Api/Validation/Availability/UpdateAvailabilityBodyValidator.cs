using Core.Domain.Dtos.Availability;
using Core.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Api.Validation.Availability
{
    public class UpdateAvailabilityBodyValidator : AbstractValidator<UpdateAvailabilityBody>
    {
        public UpdateAvailabilityBodyValidator()
        {
            const string availabilityStatusIdName = nameof(UpdateAvailabilityBody.AvailabilityStatusId);
            IEnumerable<int> values = Enum
                .GetValues(typeof(AvailabilityStatusId))
                .Cast<int>();

            string valuesString = string.Join(", ", values);

            RuleFor(body => body.AvailabilityStatusId)
                .NotEmpty()
                .WithMessage($"'{availabilityStatusIdName}' must not be empty")
                .IsInEnum()
                .WithMessage($"'{availabilityStatusIdName}' must be one of the following values: {valuesString}");
        }
    }
}