using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships
{
    public class UpdateFriendshipStatusBodyExample : IMultipleExamplesProvider<UpdateFriendshipStatusBody>
    {
        public IEnumerable<SwaggerExample<UpdateFriendshipStatusBody>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<UpdateFriendshipStatusBody>
                {
                    Name = "SetFriendshipPending",
                    Summary = "Set friendship to pending",
                    Value = new UpdateFriendshipStatusBody
                    {
                        FriendshipStatusId = FriendshipStatusId.Pending,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusBody>
                {
                    Name = "AcceptFriendship",
                    Summary = "Accept friendship request",
                    Value = new UpdateFriendshipStatusBody
                    {
                        FriendshipStatusId = FriendshipStatusId.Accepted,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusBody>
                {
                    Name = "IgnoreFriendship",
                    Summary = "Ignore friendship request",
                    Value = new UpdateFriendshipStatusBody
                    {
                        FriendshipStatusId = FriendshipStatusId.Ignored,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusBody>
                {
                    Name = "BlockUser",
                    Summary = "Block the requesting user",
                    Value = new UpdateFriendshipStatusBody
                    {
                        FriendshipStatusId = FriendshipStatusId.Blocked,
                    }
                },
            };
        }
    }
}
