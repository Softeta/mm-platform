using Microsoft.AspNetCore.Mvc;

namespace API.Customization.Exceptions
{
    public class ExceptionResponse : ValidationProblemDetails
    {
        public string? Code { get; set; }
        public string[] Parameters { get; set; } = new string[] { };
        public string? StackTrace { get; set; }

        public ExceptionResponse(
            string title, 
            int status,
            Dictionary<string, string[]> errors,
            string? code = null,
            string[]? parameters = null) : base(errors)
        {
            Title = title;
            Status = status;
            Code = code;
            Parameters = parameters ?? new string[] { };
        }
    }
}
