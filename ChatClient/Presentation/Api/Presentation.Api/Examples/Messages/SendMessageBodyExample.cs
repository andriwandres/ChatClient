using Core.Domain.Dtos.Messages;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;

namespace Presentation.Api.Examples.Messages
{
    public class SendMessageBodyExample : IMultipleExamplesProvider<SendMessageBody>
    {
        public IEnumerable<SwaggerExample<SendMessageBody>> GetExamples()
        {
            return new[]
            {
                new SwaggerExample<SendMessageBody>
                {
                    Name = "SendMessage",
                    Summary = "Send message",
                    Value = new SendMessageBody
                    {
                        RecipientId = 1,
                        ParentId = null,
                        HtmlContent = "<p>Hello World!</p>"
                    }
                },
                new SwaggerExample<SendMessageBody>
                {
                    Name = "AnswerMessage",
                    Summary = "Answer message",
                    Value = new SendMessageBody
                    {
                        RecipientId = 1,
                        ParentId = 1,
                        HtmlContent = "<p>Hello World!</p>"
                    }
                }
            };
        }
    }
}
