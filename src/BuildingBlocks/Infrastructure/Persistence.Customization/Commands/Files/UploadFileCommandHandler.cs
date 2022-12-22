using Azure.Data.Tables;
using MediatR;
using Persistence.Customization.FileStorage.Clients;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;

namespace Persistence.Customization.Commands.Files
{
    public class UploadFileCommandHandler<TUploadClient> : IRequestHandler<UploadFileCommand<TUploadClient>, Guid> 
        where TUploadClient : IFileUploadClient
    {
        private readonly TUploadClient _fileUploadClient;
        private readonly TableClient _fileCacheTableClient;

        public UploadFileCommandHandler(
            TUploadClient fileUploadClient,
            IPrivateTableServiceClient privateTableServiceClient)
        {
            _fileUploadClient = fileUploadClient;
            _fileCacheTableClient = privateTableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task<Guid> Handle(UploadFileCommand<TUploadClient> request, CancellationToken cancellationToken)
        {
            var cacheId = Guid.NewGuid();

            var filePath = await _fileUploadClient.ExecuteAsync(
                request.File,
                request.ContainerName,
                cacheId.ToString(),
                cancellationToken);

            if (filePath is null)
            {
                throw new Exception($"Uploaded file path is null.");
            }

            var fileCache = new FileCacheEntity
            {
                PartitionKey = request.TablePartitionKey,
                RowKey = cacheId.ToString(),
                FileName = request.File.FileName,
                Category = request.Category,
                FullFilePath = filePath,
                ExpirationDate = DateTimeOffset.UtcNow.AddMinutes(request.ExpiresInMinutes)
            };

            await _fileCacheTableClient.AddEntityAsync(fileCache, cancellationToken);

            return cacheId;
        }
    }
}
