using API.Customization.Extensions;
using API.Customization.HttpContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace API.Customization.Exceptions
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ExceptionOptions _options;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            IOptions<ExceptionOptions> options, 
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            _options = options.Value;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request: {HttpMethod} {Path}. ObjectIdentifier: {ObjectIdentifier}. Scope: {ScopeIdentifier}", httpContext.Request.Method, httpContext.Request.Path, httpContext.User.UserId(), httpContext.User.Scope());
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var isExceptionHandled = exception.Message.TryParseJson<ExceptionResponse>(out var result);
            if (isExceptionHandled)
            {
                await context.WriteHandledExceptionToResponseAsync(exception, result, _options.IsStackTraceOn);
            }
            else 
            {
                await context.WriteUnhandledExceptionToResponseAsync(exception, _options.IsStackTraceOn);              
            }       
        }
    }
}
