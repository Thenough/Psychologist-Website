using System.Collections.Generic;

namespace Core.Models
{
    public class ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public string Message { get; set; }
    }
}