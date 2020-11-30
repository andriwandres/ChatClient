using Core.Domain.Resources.Messages;
using Swashbuckle.AspNetCore.Filters;
using System;

namespace Presentation.Api.Examples.Messages
{
    public class GetMessageByIdOkExample : IExamplesProvider<MessageResource>
    {
        public MessageResource GetExamples()
        {
            return new MessageResource
            {
                MessageId = 1,
                MessageRecipientId = 1,
                AuthorId = 1,
                ParentId = null,
                AuthorName = "joseph_bogard",
                HtmlContent = "Hello world!",
                Created = new DateTime(2020, 1, 16, 13, 45, 58),
                ReadDate = new DateTime(2020, 1, 16, 14, 2, 33),
                IsRead = true,
                IsEdited = false,
                IsOwnMessage = false
            };
        }
    }
}
