using System.Net;

namespace Domain.Seedwork.Exceptions
{
    public class HttpException : BaseException
    {
        public HttpStatusCode? StatusCode { get; set; }

        public HttpException(string message, HttpStatusCode? statusCode = null, string? code = null, string[]? parameters = null) : base(message, code, parameters)
        {
            StatusCode = statusCode;
        }
    }
}
