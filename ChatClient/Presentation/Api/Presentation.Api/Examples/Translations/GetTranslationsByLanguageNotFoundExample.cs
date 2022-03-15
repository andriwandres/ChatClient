﻿using Core.Domain.Resources.Errors;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Filters;

namespace Presentation.Api.Examples.Translations;

public class GetTranslationsByLanguageNotFoundExample : IExamplesProvider<ErrorResource>
{
    public ErrorResource GetExamples()
    {
        return new ErrorResource
        {
            StatusCode = StatusCodes.Status404NotFound,
            Message = "Language with ID 'xxx' does not exist"
        };
    }
}