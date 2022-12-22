using API.Customization.HttpContexts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace API.Customization.Loggings
{
    public class LoggingScopeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingScopeMiddleware> _logger;

        public LoggingScopeMiddleware(RequestDelegate next, ILogger<LoggingScopeMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            using var scope = _logger.BeginScope(
                new Dictionary<string, string>() {
                    { "ObjectIdentifier", httpContext.User.UserId().ToString() },
                    { "ScopeIdentifier", httpContext.User.Scope()}
                }
            );

            await _next(httpContext);
        }
    }
}
