﻿using Core.Domain.Dtos.Availability;
using Core.Domain.Enums;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Api.Examples.Availability
{
    public class UpdateAvailabilityBadRequestExample : IExamplesProvider<ValidationErrorResource>
    {
        public ValidationErrorResource GetExamples()
        {
            const string availabilityStatusName = nameof(UpdateAvailabilityBody.AvailabilityStatus);

            IEnumerable<int> values = Enum
                .GetValues(typeof(AvailabilityStatus))
                .Cast<int>();

            string valuesString = string.Join(", ", values);

            return new ValidationErrorResource
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "One or multiple validation errors occurred",
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        availabilityStatusName,
                        new []
                        {
                            $"'{availabilityStatusName}' must be one of the following values: {valuesString}"
                        }
                    }
                }
            };
        }
    }
}
