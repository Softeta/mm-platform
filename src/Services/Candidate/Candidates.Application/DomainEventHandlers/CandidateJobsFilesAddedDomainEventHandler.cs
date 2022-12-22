using Azure.Data.Tables;
using Candidates.Domain.Events.CandidateJobsAggregate;
using MediatR;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;

namespace Candidates.Application.DomainEventHandlers
{
    internal class CandidateJobsFilesAddedDomainEventHandler :
        INotificationHandler<CandidateJobsFilesAddedDomainEvent>
    {
        private readonly TableClient _fileTableClient;

        public CandidateJobsFilesAddedDomainEventHandler(
            IPrivateTableServiceClient fileTableClient)
        {
            _fileTableClient = fileTableClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task Handle(CandidateJobsFilesAddedDomainEvent notification, CancellationToken cancellationToken)
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
