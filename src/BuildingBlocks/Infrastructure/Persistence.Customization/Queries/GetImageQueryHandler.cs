using Azure.Data.Tables;
using Domain.Seedwork.Enums;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Linq;

namespace Persistence.Customization.Queries
{
    public class GetImageQueryHandler : IRequestHandler<GetImageQuery, Dictionary<ImageType, string?>?>
    {
        private readonly TableClient _fileTableClient;

        public GetImageQueryHandler(IPrivateTableServiceClient tableServiceClient)
        {
            _fileTableClient = tableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task<Dictionary<ImageType, string?>?> Handle(GetImageQuery request, CancellationToken cancellationToken)
        {
            if (request.CacheId is null)
            {
                return null;
            }

            var imagePaths = new Dictionary<ImageType, string?>();

            var fileEntity = await _fileTableClient.GetEntityAsync<FileCacheEntity>(
                request.TablePartitionKey,
                request.CacheId.ToString(),
                cancellationToken: cancellationToken);

            var filePaths = fileEntity.Value.FullFilePath.Split(";");

            foreach (var filePath in filePaths)
            {
                var imageTypeFromFilePath = filePath.Split("/").SkipLast(1).Last();
                if (Enum.TryParse<ImageType>(imageTypeFromFilePath, out var imageType))
                {
                    imagePaths.Add(imageType, filePath);
                }
            }

            return imagePaths;
        }
    }
}
