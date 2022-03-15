using System.Text.Json.Serialization;

namespace Core.Domain.Resources.Errors;

public class ErrorResource
{
    [JsonPropertyName("statusCode")]    
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}