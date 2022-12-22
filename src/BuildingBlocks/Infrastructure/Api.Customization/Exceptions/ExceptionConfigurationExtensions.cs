using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Customization.Exceptions
{
    public static class ExceptionConfigurationExtensions
    {
        public static WebApplicationBuilder ConfigureExceptionOptions(this WebApplicationBuilder builder)
        {
            var exceptionOptions = builder.Configuration.GetExceptionOptions();

            builder.Services.Configure<ExceptionOptions>(options =>
            {
                options.IsStackTraceOn = exceptionOptions.IsStackTraceOn;
            });

            return builder;
        }

        private static ExceptionOptions GetExceptionOptions(this ConfigurationManager configuration)
        {
            return configuration.GetSection("ExceptionOptions").Get<ExceptionOptions>();
        }
    }
}
