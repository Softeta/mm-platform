using Azure.Data.Tables;
using MediatR;
using Persistence.Customization.FileStorage.Clients;
using Persistence.Customization.FileStorage.Constants;
using Persistence.Customization.ImageProcessing;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Text;

namespace Persistence.Customization.Commands.Files
{
    public class UploadImageCommandHandler<TUploadClient> : IRequestHandler<UploadImageCommand<TUploadClient>, Guid>
        where TUploadClient : IFileUploadClient
    {
        private readonly IImageProcessor _imageProcessor;
        private readonly TUploadClient _fileUploadClient;
        private readonly TableClient _fileCacheTableClient;

        public UploadImageCommandHandler(
            IImageProcessor imageProcessor,
            TUploadClient fileUploadClient,
            IPrivateTableServiceClient privateTableServiceClient)
        {
            _imageProcessor = imageProcessor;
            _fileUploadClient = fileUploadClient;
            _fileCacheTableClient = privateTableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task<Guid> Handle(UploadImageCommand<TUploadClient> request, CancellationToken cancellationToken)
        {
            var cacheId = Guid.NewGuid();

            await using var memoryStream = new MemoryStream();
            await request.Image.CopyToAsync(memoryStream, cancellationToken);

            var processedImages = await _imageProcessor.ExecuteAsync(
                memoryStream,
                cancellationToken);

            var combinedFilePaths = new StringBuilder();

            foreach (var processedImage in processedImages)
            {
                if (combinedFilePaths.Length > 0) combinedFilePaths.Append(';');

                var container = $"{request.ContainerName}/{cacheId}/{processedImage.Type}";

                var imagePath = await _fileUploadClient.ExecuteAsync(
                    processedImage.Stream,
                    FileExtensions.ImageExtension,
                    container,
                    cancellationToken);

                combinedFilePaths.Append(imagePath);

                if (imagePath is null)
                {
                    throw new Exception($"Uploaded image file path is null.");
                }
            }

            var fileCache = new FileCacheEntity
            {
                PartitionKey = request.TablePartitionKey,
                RowKey = cacheId.ToString(),
                FileName = request.Image.FileName,
                Category = request.Category,
                FullFilePath = combinedFilePaths.ToString(),
                ExpirationDate = DateTimeOffset.UtcNow.AddMinutes(request.ExpiresInMinutes)
            };

            await _fileCacheTableClient.AddEntityAsync(fileCache, cancellationToken);
            return cacheId;
        }
    }
}

