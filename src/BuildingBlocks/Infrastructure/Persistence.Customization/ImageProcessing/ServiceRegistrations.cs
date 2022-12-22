using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Customization.ImageProcessing
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddImageProcessor(this IServiceCollection services)
        {
            services.AddSingleton<IImageProcessor, ImageProcessor>();
            return services;
        }
    }
}
