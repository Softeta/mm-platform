using AdministrationSettings.API.Models.Configurations;

namespace AdministrationSettings.API.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
        {
            var imageSettings = builder.Configuration.GetSection("ImageSettings").Get<FileSettings>();
            var videoSettings = builder.Configuration.GetSection("VideoSettings").Get<FileSettings>();
            var documentSettings = builder.Configuration.GetSection("DocumentSettings").Get<FileSettings>();

            var configurationResponse = new Configurations
            {
                ImageSettings = imageSettings,
                VideoSettings = videoSettings,
                DocumentSettings = documentSettings
            };

            builder.Services.AddSingleton(configurationResponse);

            return builder;
        }
    }
}
