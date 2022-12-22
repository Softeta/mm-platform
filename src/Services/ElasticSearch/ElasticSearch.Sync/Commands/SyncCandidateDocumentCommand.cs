using ElasticSearch.Sync.Events;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal record SyncCandidateDocumentCommand(
        string FilterName,
        CandidateChangedEvent CandidateChangedEvent) : INotification;
}
