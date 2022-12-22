using API.Customization.Exceptions;
using API.Customization.Loggings;
using Microsoft.AspNetCore.Builder;

namespace API.Customization.Middleware
{
    public static class MiddlewareExtensions
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        public static void UseLoggingScope(this IApplicationBuilder app)
        {
            app.UseMiddleware<LoggingScopeMiddleware>();
        }
    }
}
