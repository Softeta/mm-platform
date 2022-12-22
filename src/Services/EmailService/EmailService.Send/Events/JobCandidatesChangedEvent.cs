using EmailService.Send.Events.JobCandidates;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class JobCandidatesChangedEvent : Event<JobCandidateChangedPayload>
    {
    }
}
