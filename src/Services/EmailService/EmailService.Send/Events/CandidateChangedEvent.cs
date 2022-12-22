using EmailService.Send.Events.Candidates;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class CandidateChangedEvent: Event<CandidateChangedPayload>
    {
    }
}
