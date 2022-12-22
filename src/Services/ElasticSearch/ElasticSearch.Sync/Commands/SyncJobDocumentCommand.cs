using ElasticSearch.Sync.Events;
using MediatR;

namespace ElasticSearch.Sync.Commands;

internal record SyncJobDocumentCommand(
    string FilterName,
    JobChangedEvent JobChangedEvent) : INotification;