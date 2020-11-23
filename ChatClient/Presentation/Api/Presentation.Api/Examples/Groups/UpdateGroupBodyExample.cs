using Core.Domain.Dtos.Groups;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Groups
{
    public class UpdateGroupBodyExample : IExamplesProvider<UpdateGroupBody>
    {
        public UpdateGroupBody GetExamples()
        {
            return new UpdateGroupBody
            {
                Name = "Saturday night with friends",
                Description = "This group chat is for planning the upcoming saturday night"
            };
        }
    }
}
