using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public record UpdateFileCommand<TDeleteClient, TUploadClient>(
        string ContainerName,
        string TablePartitionKey,
        string Category,
        int ExpiresInMinutes,
        IFormFile File,
        Guid CacheId
        ) : IRequest<Guid>
        where TDeleteClient : IFileDeleteClient
        where TUploadClient : IFileUploadClient;
}
