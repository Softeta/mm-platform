using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;
using Persistence.Customization.Storages.Builders;

namespace Persistence.Customization.FileStorage.Clients
{
    public abstract class FileUploadClient : IFileUploadClient
    {
        private readonly StorageAccountConfigurations _configurations;

        protected FileUploadClient(IOptions<StorageAccountConfigurations> configurations)
        {
            _configurations = configurations.Value;
        }

        public async Task<string?> ExecuteAsync(
            Stream stream,
            string fileExtension,
            string containerName,
            CancellationToken cancellation = default)
        {
            var fileName = $"{Guid.NewGuid()}{fileExtension}";

            var blobClient = new BlobClientBuilder()
                .ForStorageAccount(_configurations.ConnectionString)
                .WithContainer(containerName)
                .WithFileName(fileName)
                .Build();

            stream.Position = 0;
            await blobClient.UploadAsync(stream, cancellation);
            return $"{_configurations.FileStorageUrl}{containerName}/{fileName}";
        }

        public async Task<string?> ExecuteAsync(
            IFormFile file,
            string containerName,
            string? folder,
            CancellationToken cancellationToken = default)
        {
            var container = !string.IsNullOrEmpty(folder) ? $"{containerName}/{folder}" : $"{containerName}";

            await using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream, cancellationToken);

            var filePath = await ExecuteAsync(
                memoryStream,
                new FileInfo(file.FileName).Extension,
                container,
                cancellationToken);

            return filePath;
        }
    }
}
