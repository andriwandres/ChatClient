﻿using Core.Domain.ViewModels.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.GroupMemberships;

public class UpdateMembershipForbiddenExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "You are not permitted to mutate users in this group. This privilege is only granted to administrators of the group"
        };
    }
}