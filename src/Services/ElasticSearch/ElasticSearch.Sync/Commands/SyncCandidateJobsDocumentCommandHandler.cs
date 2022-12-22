using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ElasticSearch.Shared.Clients;
using ElasticSearch.Sync.Indexes;
using ElasticSearch.Sync.Indexes.Candidates;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal class SyncCandidateJobsDocumentCommandHandler : INotificationHandler<SyncCandidateJobsDocumentCommand>
    {
        private readonly ICandidatesSearchClient _serviceClient;

        public SyncCandidateJobsDocumentCommandHandler(ICandidatesSearchClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task Handle(SyncCandidateJobsDocumentCommand notification, CancellationToken cancellationToken)
        {
            if (notification.CandidateJobsChangedEvent.Payload is null)
            {
                throw new InvalidDataContractException();
            }

            var document = CandidateJobs.FromEvent(notification.CandidateJobsChangedEvent.Payload);

            await _serviceClient.SyncDocumentAsync(document);
        }
    }
}
