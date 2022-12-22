using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using ElasticSearch.Shared.Clients;
using ElasticSearch.Sync.Indexes.Jobs;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal class SyncAppliedCandidatesDocumentCommandHandler : BaseJobDocumentHandler, INotificationHandler<SyncAppliedCandidatesDocumentCommand>
    {
        private readonly IJobsSearchClient _jobsSearchClient;

        public SyncAppliedCandidatesDocumentCommandHandler(IJobsSearchClient jobsSearchClient)
        {
            _jobsSearchClient = jobsSearchClient;
        }

        public async Task Handle(SyncAppliedCandidatesDocumentCommand notification, CancellationToken cancellationToken)
        {
            var payload = notification.JobCandidatesChangedEvent.Payload;

            if (payload is null)
            {
                throw new InvalidDataContractException();
            }

            if (!IsValid(payload.Stage))
            {
                return;
            }

            var document = AppliedCandidates.FromEvent(payload);

            await _jobsSearchClient.SyncDocumentAsync(document);
        }
    }
}
