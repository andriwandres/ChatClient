﻿using Core.Domain.ViewModels.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Messages;

public class GetMessageByIdForbiddenExample : IExamplesProvider<ErrorViewModel>
{
    public ErrorViewModel GetExamples()
    {
        return new ErrorViewModel
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = "The user is not permitted to see this message. He may only see messages that he received or wrote himself"
        };
    }
}