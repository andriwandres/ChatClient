using Core.Domain.Dtos.Groups;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Groups
{
    public class UpdateGroupRequestExample : IExamplesProvider<UpdateGroupDto>
    {
        public UpdateGroupDto GetExamples()
        {
            return new UpdateGroupDto
            {
                Name = "Saturday night with friends",
                Description = "This group chat is for planning the upcoming saturday night"
            };
        }
    }
}
