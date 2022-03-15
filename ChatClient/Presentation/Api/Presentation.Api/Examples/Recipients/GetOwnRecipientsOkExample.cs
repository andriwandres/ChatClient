using Core.Domain.Enums;
using Core.Domain.Resources.Groups;
using Core.Domain.Resources.Messages;
using Core.Domain.Resources.Recipients;
using Core.Domain.Resources.Users;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Recipients;

public class GetOwnRecipientsOkExample : IExamplesProvider<IEnumerable<RecipientResource>>
{
    public IEnumerable<RecipientResource> GetExamples()
    {
        return new[]
        {
            new RecipientResource
            {
                RecipientId = 2,
                UnreadMessagesCount = 2,
                AvailabilityStatus = AvailabilityStatus.Online,
                TargetUser = new TargetUserResource
                {
                    UserId = 2,
                    UserName = "joseph_bogard"
                },
                TargetGroup = null,
                LatestMessage = new LatestMessageResource
                {
                    MessageId = 3,
                    MessageRecipientId = 2,
                    AuthorId = 2,
                    AuthorName = "joseph_bogard",
                    HtmlContent = "Are you still there?",
                    IsRead = false,
                    IsOwnMessage = false,
                    Created = new DateTime(2020, 8, 15, 21, 02, 55)
                }
            },
            new RecipientResource
            {
                RecipientId = 3,
                UnreadMessagesCount = 0,
                TargetUser = null,
                AvailabilityStatus = 0,
                TargetGroup = new TargetGroupResource
                {
                    GroupId = 1,
                    Name = "Saturday night with friends"
                },
                LatestMessage = new LatestMessageResource
                {
                    MessageId = 1,
                    MessageRecipientId = 1,
                    AuthorId = 1,
                    AuthorName = "alfred_miller",
                    HtmlContent = "Hello guys, how are you?",
                    IsRead = true,
                    IsOwnMessage = true,
                    Created = new DateTime(2020, 8, 15, 18, 25, 13)
                }
            }
        };
    }
}