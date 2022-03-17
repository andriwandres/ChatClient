using Swashbuckle.AspNetCore.Filters;
using System;
using Core.Domain.ViewModels.Groups;

namespace Presentation.Api.Examples.Groups;

public class GetGroupByIdOkExample : IExamplesProvider<GroupViewModel>
{
    public GroupViewModel GetExamples()
    {
        return new GroupViewModel
        {
            GroupId = 1,
            Name = "Saturday night with friends",
            Description = "This group chat is for planning the upcoming saturday night",
            Created = new DateTime(2020, 4, 15, 14, 31, 58)
        };
    }
}