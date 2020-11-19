using Core.Domain.Dtos.Groups;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Groups
{
    public class CreateGroupRequestExample : IExamplesProvider<CreateGroupDto>
    {
        public CreateGroupDto GetExamples()
        {
            return new CreateGroupDto
            {
                Name = "Saturday night with friends",
                Description = "This group chat is for planning the upcoming saturday night"
            };
        }
    }
}
