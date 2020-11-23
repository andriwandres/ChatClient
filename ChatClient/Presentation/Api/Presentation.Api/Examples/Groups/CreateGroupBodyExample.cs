using Core.Domain.Dtos.Groups;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Groups
{
    public class CreateGroupBodyExample : IExamplesProvider<CreateGroupBody>
    {
        public CreateGroupBody GetExamples()
        {
            return new CreateGroupBody
            {
                Name = "Saturday night with friends",
                Description = "This group chat is for planning the upcoming saturday night"
            };
        }
    }
}
