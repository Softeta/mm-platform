using ElasticSearch.Sync.Events.Models.JobChanged;
using EventBus.Events;

namespace ElasticSearch.Sync.Events;

internal class JobChangedEvent : Event<JobChangedPayload>
{
    
}