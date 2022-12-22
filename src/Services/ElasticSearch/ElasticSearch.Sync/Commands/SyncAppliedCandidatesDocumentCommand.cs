using ElasticSearch.Sync.Events;
using MediatR;

namespace ElasticSearch.Sync.Commands
{
    internal record SyncAppliedCandidatesDocumentCommand(
        string FilterName,
        JobCandidatesChangedEvent JobCandidatesChangedEvent) : INotification;
}
