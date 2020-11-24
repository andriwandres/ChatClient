using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
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
            const string friendshipStatusIdName = nameof(UpdateFriendshipStatusBody.FriendshipStatusId);
            IEnumerable<int> values = Enum
                .GetValues(typeof(FriendshipStatusId))
                .Cast<int>();

            string valuesString = string.Join(", ", values);

            RuleFor(model => model.FriendshipStatusId)
                .NotEmpty()
                .WithMessage($"'{friendshipStatusIdName}' must be one of the following values: {valuesString}")
                .IsInEnum()
                .WithMessage($"'{friendshipStatusIdName}' must be one of the following values: {valuesString}");
        }
    }
}
