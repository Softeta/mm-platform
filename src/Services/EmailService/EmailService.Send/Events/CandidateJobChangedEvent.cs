using EmailService.Send.Events.CandidateJobs;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class CandidateJobChangedEvent : Event<CandidateJobPayload>
    {
    }
}
