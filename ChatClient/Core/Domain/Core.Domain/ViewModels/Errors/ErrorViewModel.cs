using System.Text.Json.Serialization;

namespace Core.Domain.ViewModels.Errors;

public class ErrorViewModel
{
    [JsonPropertyName("statusCode")]    
    public int StatusCode { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}