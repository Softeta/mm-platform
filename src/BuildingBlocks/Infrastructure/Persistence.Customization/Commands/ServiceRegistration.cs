using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Private;
using Persistence.Customization.FileStorage.Clients.Public;

namespace Persistence.Customization.Commands
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddGenericCommands(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRequestHandler<UploadFileCommand<IPrivateFileUploadClient>, Guid>), typeof(UploadFileCommandHandler<IPrivateFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UploadFileCommand<IPublicFileUploadClient>, Guid>), typeof(UploadFileCommandHandler<IPublicFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UpdateFileCommand<IPrivateFileDeleteClient, IPrivateFileUploadClient>, Guid>), typeof(UpdateFileCommandHandler<IPrivateFileDeleteClient, IPrivateFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UpdateFileCommand<IPublicFileDeleteClient, IPublicFileUploadClient>, Guid>), typeof(UpdateFileCommandHandler<IPublicFileDeleteClient, IPublicFileUploadClient>));
            services.AddScoped(typeof(INotificationHandler<DeleteFileCommand<IPrivateFileDeleteClient>>), typeof(DeleteFileCommandHandler<IPrivateFileDeleteClient>));
            services.AddScoped(typeof(INotificationHandler<DeleteFileCommand<IPublicFileDeleteClient>>), typeof(DeleteFileCommandHandler<IPublicFileDeleteClient>));
            services.AddScoped(typeof(IRequestHandler<UploadImageCommand<IPrivateFileUploadClient>, Guid>), typeof(UploadImageCommandHandler<IPrivateFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UploadImageCommand<IPublicFileUploadClient>, Guid>), typeof(UploadImageCommandHandler<IPublicFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UpdateImageCommand<IPrivateFileDeleteClient, IPrivateFileUploadClient>, Guid>), typeof(UpdateImageCommandHandler<IPrivateFileDeleteClient, IPrivateFileUploadClient>));
            services.AddScoped(typeof(IRequestHandler<UpdateImageCommand<IPublicFileDeleteClient, IPublicFileUploadClient>, Guid>), typeof(UpdateImageCommandHandler<IPublicFileDeleteClient, IPublicFileUploadClient>));
            services.AddScoped(typeof(INotificationHandler<DeleteFilesBatchCommand<IPublicFileDeleteClient>>), typeof(DeleteFilesBatchCommandHandler<IPublicFileDeleteClient>));
            services.AddScoped(typeof(INotificationHandler<DeleteFilesBatchCommand<IPrivateFileDeleteClient>>), typeof(DeleteFilesBatchCommandHandler<IPrivateFileDeleteClient>));
            return services;
        }
    }
}
