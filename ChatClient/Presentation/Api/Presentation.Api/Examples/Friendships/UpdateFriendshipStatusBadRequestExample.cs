﻿using Core.Domain.Dtos.Friendships;
using Core.Domain.Enums;
using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Api.Examples.Friendships
{
    public class UpdateFriendshipStatusBadRequestExample : IExamplesProvider<ValidationErrorResource>
    {
        public ValidationErrorResource GetExamples()
        {
            const string friendshipStatusName = nameof(UpdateFriendshipStatusBody.FriendshipStatus);
            IEnumerable<int> values = Enum
                .GetValues(typeof(FriendshipStatus))
                .Cast<int>();

            string valuesString = string.Join(", ", values);

            return new ValidationErrorResource
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "One or multiple validation errors occurred",
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        friendshipStatusName,
                        new[]
                        {
                            $"'{friendshipStatusName}' must be one of the following values: {valuesString}"
                        }
                    }
                }
            };
        }
    }
}
