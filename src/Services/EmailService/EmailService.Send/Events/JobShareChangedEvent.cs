using EmailService.Send.Events.JobShare;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class JobShareChangedEvent : Event<JobShareChangedPayload>
    {
    }
}
