using ElasticSearch.Shared.Clients;
using ElasticSearch.Sync.Indexes.Jobs;
using MediatR;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticSearch.Sync.Commands;

internal class SyncJobDocumentCommandHandler : BaseJobDocumentHandler, INotificationHandler<SyncJobDocumentCommand>
{
    private readonly IJobsSearchClient _jobsSearchClient;

    public SyncJobDocumentCommandHandler(IJobsSearchClient jobsSearchClient)
    {
        _jobsSearchClient = jobsSearchClient;
    }

    public async Task Handle(SyncJobDocumentCommand notification, CancellationToken cancellationToken)
    {
        var payload = notification.JobChangedEvent.Payload;

        if (payload is null)
        {
            throw new InvalidDataContractException();
        }

        if (!IsValid(payload.Stage))
        {
            return;
        }

        var document = Job.FromEvent(payload);
        await _jobsSearchClient.SyncDocumentAsync(document);
    }
}