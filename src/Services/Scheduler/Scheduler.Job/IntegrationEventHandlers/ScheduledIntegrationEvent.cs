using EventBus.EventHandlers;
using System;

namespace Scheduler.Job.IntegrationEventHandlers
{
    public class ScheduledIntegrationEvent : IntegrationEvent
    {
        public ScheduledIntegrationEvent(DateTimeOffset emittedAt) : base(emittedAt)
        {
        }
    }
}
