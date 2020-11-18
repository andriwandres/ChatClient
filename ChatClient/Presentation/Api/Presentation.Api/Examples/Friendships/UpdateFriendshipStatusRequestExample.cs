using Core.Domain.Dtos.Friendships;
using Core.Domain.Entities;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Friendships
{
    public class UpdateFriendshipStatusRequestExample : IMultipleExamplesProvider<UpdateFriendshipStatusDto>
    {
        public IEnumerable<SwaggerExample<UpdateFriendshipStatusDto>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<UpdateFriendshipStatusDto>
                {
                    Name = "SetFriendshipPending",
                    Summary = "Set friendship to pending",
                    Value = new UpdateFriendshipStatusDto
                    {
                        FriendshipStatusId = FriendshipStatusId.Pending,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusDto>
                {
                    Name = "AcceptFriendship",
                    Summary = "Accept friendship request",
                    Value = new UpdateFriendshipStatusDto
                    {
                        FriendshipStatusId = FriendshipStatusId.Accepted,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusDto>
                {
                    Name = "IgnoreFriendship",
                    Summary = "Ignore friendship request",
                    Value = new UpdateFriendshipStatusDto
                    {
                        FriendshipStatusId = FriendshipStatusId.Ignored,
                    }
                },
                new SwaggerExample<UpdateFriendshipStatusDto>
                {
                    Name = "BlockUser",
                    Summary = "Block the requesting user",
                    Value = new UpdateFriendshipStatusDto
                    {
                        FriendshipStatusId = FriendshipStatusId.Blocked,
                    }
                },
            };
        }
    }
}
