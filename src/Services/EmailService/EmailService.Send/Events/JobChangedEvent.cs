using EmailService.Send.Events.Jobs;
using EventBus.Events;

namespace EmailService.Send.Events
{
    internal class JobChangedEvent : Event<JobChangedPayload>
    {
    }
}

