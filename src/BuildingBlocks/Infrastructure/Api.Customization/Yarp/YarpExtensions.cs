using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace API.Customization.Yarp
{
    public static class YarpExtensions
    {
        public static WebApplicationBuilder AddYarpConfiguration(this WebApplicationBuilder builder)
        {
             builder.Configuration
                .AddJsonFile("yarp.json")
                .AddJsonFile($"yarp.{builder.Environment.EnvironmentName}.json", true)
                .AddEnvironmentVariables()
                .Build();

            return builder;
        }

        public static IConfigurationSection GetYarpConfiguration(this WebApplicationBuilder builder)
        {
            return builder.Configuration.GetSection("Yarp");
        }
    }
}
