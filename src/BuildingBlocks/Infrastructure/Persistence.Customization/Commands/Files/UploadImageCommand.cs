using MediatR;
using Microsoft.AspNetCore.Http;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public record UploadImageCommand<TUploadClient>(
        IFormFile Image,
        string ContainerName,
        string TablePartitionKey,
        string Category,
        int ExpiresInMinutes)
        : IRequest<Guid> where TUploadClient : IFileUploadClient;
}
