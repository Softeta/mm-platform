using Azure.Data.Tables;
using Candidates.Domain.Events.CandidateAggregate;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;

namespace Candidates.Application.DomainEventHandlers
{
    internal class CandidateFilesAddedDomainEventHandler :
        INotificationHandler<CandidateFilesAddedDomainEvent>
    {
        private readonly TableClient _fileTableClient;

        public CandidateFilesAddedDomainEventHandler(
            IPrivateTableServiceClient tableServiceClient)
        {
            _fileTableClient = tableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task Handle(CandidateFilesAddedDomainEvent notification, CancellationToken cancellationToken)
        {
            await Parallel.ForEachAsync(notification.CacheIds, cancellationToken, async (cacheId, _) =>
            {
                await _fileTableClient.DeleteEntityAsync(
                    FileCacheTableStorage.Candidate.FilePartitionKey,
                    cacheId.ToString(),
                    cancellationToken: cancellationToken);
            });
        }
    }
}
