using EmailService.Sync.Events.Candidate;
using EventBus.Events;

namespace EmailService.Sync.Events;

internal class CandidateChangedEvent : Event<CandidatePayload>
{
    
}