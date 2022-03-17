using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Domain.ViewModels.Errors;

public class ValidationErrorViewModel
{
    [JsonPropertyName("statusCode")]
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("errors")]
    public IDictionary<string, IEnumerable<string>> Errors { get; set; }
}