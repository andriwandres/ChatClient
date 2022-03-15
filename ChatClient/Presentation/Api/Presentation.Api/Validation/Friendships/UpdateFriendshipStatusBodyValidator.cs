using Core.Domain.Dtos.Friendships;
using Core.Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Api.Validation.Friendships
{
    public class UpdateFriendshipStatusBodyValidator : AbstractValidator<UpdateFriendshipStatusBody>
    {
        public UpdateFriendshipStatusBodyValidator()
        {
            const string friendshipStatusName = nameof(UpdateFriendshipStatusBody.FriendshipStatus);
            IEnumerable<int> values = Enum
                .GetValues(typeof(FriendshipStatus))
                .Cast<int>();

            string valuesString = string.Join(", ", values);

            RuleFor(model => model.FriendshipStatus)
                .NotEmpty()
                .WithMessage($"'{friendshipStatusName}' must be one of the following values: {valuesString}")
                .IsInEnum()
                .WithMessage($"'{friendshipStatusName}' must be one of the following values: {valuesString}");
        }
    }
}
