using MediatR;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public record DeleteFileCommand<TDeleteClient>(
        string ContainerName,
        string TablePartitionKey,
        Guid CacheId) : INotification where TDeleteClient : IFileDeleteClient;
}
