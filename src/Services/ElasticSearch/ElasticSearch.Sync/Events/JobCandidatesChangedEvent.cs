using ElasticSearch.Sync.Events.Models.JobCandidatesChanged;
using EventBus.Events;

namespace ElasticSearch.Sync.Events;

internal class JobCandidatesChangedEvent : Event<JobCandidatesChangedPayload>
{   
}