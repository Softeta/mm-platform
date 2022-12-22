using Microsoft.AspNetCore.Http;

namespace Persistence.Customization.FileStorage.Clients;

public interface IFileUploadClient
{
    Task<string?> ExecuteAsync(
            Stream stream,
            string fileExtension,
            string containerName,
            CancellationToken cancellationToken = default);

    Task<string?> ExecuteAsync(
        IFormFile file,
        string containerName,
        string? folder,
        CancellationToken cancellationToken = default);
}