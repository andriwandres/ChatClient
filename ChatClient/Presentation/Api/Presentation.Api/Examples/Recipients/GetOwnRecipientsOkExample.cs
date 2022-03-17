using Core.Domain.Enums;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using Core.Domain.ViewModels.Groups;
using Core.Domain.ViewModels.Messages;
using Core.Domain.ViewModels.Recipients;
using Core.Domain.ViewModels.Users;

namespace Presentation.Api.Examples.Recipients;

public class GetOwnRecipientsOkExample : IExamplesProvider<IEnumerable<RecipientViewModel>>
{
    public IEnumerable<RecipientViewModel> GetExamples()
    {
        return new[]
        {
            new RecipientViewModel
            {
                RecipientId = 2,
                UnreadMessagesCount = 2,
                AvailabilityStatus = AvailabilityStatus.Online,
                TargetUser = new TargetUserViewModel
                {
                    UserId = 2,
                    UserName = "joseph_bogard"
                },
                TargetGroup = null,
                LatestMessage = new LatestMessageViewModel
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
            new RecipientViewModel
            {
                RecipientId = 3,
                UnreadMessagesCount = 0,
                TargetUser = null,
                AvailabilityStatus = 0,
                TargetGroup = new TargetGroupViewModel
                {
                    GroupId = 1,
                    Name = "Saturday night with friends"
                },
                LatestMessage = new LatestMessageViewModel
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