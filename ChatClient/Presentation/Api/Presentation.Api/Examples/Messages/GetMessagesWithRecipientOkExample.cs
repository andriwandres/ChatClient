using Core.Domain.Resources.Messages;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages
{
    public class GetMessagesWithRecipientOkExample : IExamplesProvider<IEnumerable<ChatMessageResource>>
    {
        public IEnumerable<ChatMessageResource> GetExamples()
        {
            return new[]
            {
                new ChatMessageResource
                {
                    MessageRecipientId = 1,
                    MessageId = 1,
                    AuthorName = "joseph_bogard",
                    HtmlContent = "Hi alfred, Have you got a moment?",
                    Created = new DateTime(2020, 11, 13, 18, 02, 31),
                    IsRead = true,
                    IsOwnMessage = false
                },
                new ChatMessageResource
                {
                    MessageRecipientId = 2,
                    MessageId = 2,
                    AuthorName = "alfred_miller",
                    HtmlContent = "Yeah sure, whats up?",
                    Created = new DateTime(2020, 11, 13, 18, 03, 01),
                    IsRead = false,
                    IsOwnMessage = true 
                }
            };
        }
    }
}
