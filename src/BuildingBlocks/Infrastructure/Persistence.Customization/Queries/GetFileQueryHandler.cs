using Azure.Data.Tables;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;

namespace Persistence.Customization.Queries
{
    public class GetFileQueryHandler : IRequestHandler<GetFileQuery, (string? FileUrl, string? FileName)>
    {
        private readonly TableClient _fileTableClient;

        public GetFileQueryHandler(IPrivateTableServiceClient tableServiceClient)
        {
            _fileTableClient = tableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task<(string? FileUrl, string? FileName)> Handle(GetFileQuery request, CancellationToken cancellationToken)
        {
            if (!request.CacheId.HasValue)
            {
                return (null, null);
            }

            var fileEntity = await _fileTableClient.GetEntityAsync<FileCacheEntity>(
                request.TablePartitionKey,
                request.CacheId.ToString(),
                cancellationToken: cancellationToken);

            return (fileEntity.Value.FullFilePath, fileEntity.Value.FileName);
        }
    }
}
