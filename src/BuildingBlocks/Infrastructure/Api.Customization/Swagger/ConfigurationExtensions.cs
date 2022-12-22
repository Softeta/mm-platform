using API.Customization.Authorization.Constants;
using Microsoft.Extensions.Configuration;

namespace API.Customization.Swagger
{
    public static class ConfigurationExtensions
    {
        public static SwaggerOptions GetSwaggerOptions(this ConfigurationManager configuration)
        {
            return configuration.GetSection("SwaggerOptions").Get<SwaggerOptions>();
        }
    }
}
