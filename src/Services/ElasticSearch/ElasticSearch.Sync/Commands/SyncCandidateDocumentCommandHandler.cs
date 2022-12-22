using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Domain.Seedwork.Enums;
using ElasticSearch.Shared.Clients;
using ElasticSearch.Sync.Indexes.Candidates;
using EventBus.Constants;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal class SyncCandidateDocumentCommandHandler : INotificationHandler<SyncCandidateDocumentCommand>
    {
        private readonly ICandidatesSearchClient _candidateSearchClient;

        public SyncCandidateDocumentCommandHandler(ICandidatesSearchClient candidateSearchClient)
        {
            _candidateSearchClient = candidateSearchClient;
        }

        public async Task Handle(SyncCandidateDocumentCommand notification, CancellationToken cancellationToken)
        {
            if (notification.CandidateChangedEvent.Payload is null)
            {
                throw new InvalidDataContractException();
            }

            var document = Candidate.FromEvent(notification.CandidateChangedEvent.Payload);

            switch (notification.FilterName)
            {
                case Topics.CandidateChanged.Filters.CandidateRejected:
                    await _candidateSearchClient.DeleteDocumentAsync(document);
                    break;
                case Topics.CandidateChanged.Filters.CandidateCreated:
                case Topics.CandidateChanged.Filters.CandidateApproved:
                case Topics.CandidateChanged.Filters.CandidateUpdated:
                case Topics.CandidateChanged.Filters.CandidateSkillsSynced:
                case Topics.CandidateChanged.Filters.CandidateJobPositionSynced
                when notification.CandidateChangedEvent.Payload.Status == CandidateStatus.Approved:
                    await _candidateSearchClient.SyncDocumentAsync(document);
                    break;
                default:
                    return;
            }
        }
    }
}
