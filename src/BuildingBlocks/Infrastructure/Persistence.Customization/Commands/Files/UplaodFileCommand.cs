using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public record UploadFileCommand<TUploadClient>(
        string ContainerName,
        string TablePartitionKey,
        string Category,
        int ExpiresInMinutes,
        IFormFile File
        ) : IRequest<Guid> where TUploadClient : IFileUploadClient;
}
