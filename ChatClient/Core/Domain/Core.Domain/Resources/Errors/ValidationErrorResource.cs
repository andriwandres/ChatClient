using System.Collections.Generic;

namespace Core.Domain.Resources.Errors
{
    public class ValidationErrorResource
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}
