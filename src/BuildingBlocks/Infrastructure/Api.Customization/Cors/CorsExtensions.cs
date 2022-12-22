using Microsoft.Extensions.DependencyInjection;

namespace API.Customization.Cors
{
    public static class CorsExtensions
    {
        public static void AddCors(this IServiceCollection services, CorsOptions options)
        {
            var splitCors = options.AllowedOrigins.Split(";");

            services.AddCors(corsOptions =>
            {
                corsOptions.AddDefaultPolicy(policyBuilder =>
                {
                    policyBuilder
                        .WithOrigins(splitCors)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
    }
}
