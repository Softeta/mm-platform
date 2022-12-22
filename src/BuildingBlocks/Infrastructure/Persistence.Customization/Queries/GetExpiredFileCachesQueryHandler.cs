using Azure.Data.Tables;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Text;

namespace Persistence.Customization.Queries
{
    public class GetExpiredFileCachesQueryHandler : IRequestHandler<GetExpiredFileCachesQuery, List<FileCacheEntity>>
    {
        private readonly TableClient _tableClient;

        public GetExpiredFileCachesQueryHandler(IPrivateTableServiceClient tableServiceClient)
        {
            _tableClient = tableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task<List<FileCacheEntity>> Handle(GetExpiredFileCachesQuery request, CancellationToken cancellationToken)
        {
            var expiredCache = new List<FileCacheEntity>();
            
            var filter = BuildFilter(request.TablePartitionKey);
            var videoCacheEntities = _tableClient.QueryAsync<FileCacheEntity>(filter, cancellationToken: cancellationToken);

            await foreach (var entity in videoCacheEntities)
            {
                if (entity != null)
                {
                    expiredCache.Add(entity);
                }
            }

            return expiredCache;
        }

        private static string BuildFilter(string partitionKey)
        {
            string dateFormatZulu = "yyyy-MM-ddTHH:mm:ss.fffZ";

            var date = DateTimeOffset.UtcNow
                .ToUniversalTime()
                .ToString(dateFormatZulu);

            var filter = new StringBuilder($"PartitionKey eq '{partitionKey}'");    
            filter.Append($" and ExpirationDate lt datetime'{date}'");

            return filter.ToString();
        }
    }
}
