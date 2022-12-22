using EmailService.Send.Events.JobCandidates;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class JobCandidatesShortlistActivatedEvent : Event<JobCandidatesShortlistActivatedPayload>
    {
    }
}
