using ElasticSearch.Sync.Events.Models.CandidateChanged;
using EventBus.Events;

namespace ElasticSearch.Sync.Events
{
    internal class CandidateChangedEvent : Event<CandidateChangedPayload>
    {
    }
}
