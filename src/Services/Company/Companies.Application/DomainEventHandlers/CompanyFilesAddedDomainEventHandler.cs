using Azure.Data.Tables;
using Companies.Domain.Events;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;

namespace Companies.Application.DomainEventHandlers
{
    internal class CompanyFilesAddedDomainEventHandler :
        INotificationHandler<CompanyFilesAddedDomainEvent>
    {
        private readonly TableClient _fileTableClient;

        public CompanyFilesAddedDomainEventHandler(
            IPrivateTableServiceClient tableServiceClient)
        {
            _fileTableClient = tableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task Handle(CompanyFilesAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Parallel.ForEachAsync(notification.CacheIds, cancellationToken, async (cacheId, _) =>
            {
                await _fileTableClient.DeleteEntityAsync(
                    FileCacheTableStorage.Company.FilePartitionKey,
                    cacheId.ToString(),
                    cancellationToken: cancellationToken);
            });
        }
    }
}
