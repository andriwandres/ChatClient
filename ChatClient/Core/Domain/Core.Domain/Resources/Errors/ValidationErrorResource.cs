using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Core.Domain.Resources.Errors
{
    public class ValidationErrorResource
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("errors")]
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
