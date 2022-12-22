using ElasticSearch.Sync.Events.Models.CandidateJobsChanged;
using EventBus.Events;

namespace ElasticSearch.Sync.Events
{
    internal class CandidateJobsChangedEvent : Event<CandidateJobsChangedPayload>
    {
    }
}
