using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.FileStorage.Clients.Public;

namespace Persistence.Customization.FileStorage
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddPublicFileClients(this IServiceCollection services)
        {
            services.AddTransient<IPublicFileUploadClient, PublicFileUploadClient>();
            services.AddTransient<IPublicFileDeleteClient, PublicFileDeleteClient>();

            return services;
        }

        public static IServiceCollection AddPrivateFileClients(this IServiceCollection services)
        {
            services.AddTransient<IPrivateFileUploadClient, PrivateFileUploadClient>();
            services.AddTransient<IPrivateFileDeleteClient, PrivateFileDeleteClient>();
            services.AddTransient<IPrivateFileDownloadClient, PrivateFileDownloadClient>();
            services.AddTransient<IPrivateBlobClient, PrivateBlobClient>();

            return services;
        }
    }
}
