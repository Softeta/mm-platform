using ElasticSearch.Sync.Events;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal record SyncCandidateJobsDocumentCommand(
        string FilterName,
        CandidateJobsChangedEvent CandidateJobsChangedEvent) : INotification;
}
