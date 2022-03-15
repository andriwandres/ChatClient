using Core.Domain.Dtos.Messages;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class EditMessageBodyExample : IExamplesProvider<EditMessageBody>
{
    public EditMessageBody GetExamples()
    {
        return new EditMessageBody
        {
            HtmlContent = "<p>Hello World!</p>"
        };
    }
}