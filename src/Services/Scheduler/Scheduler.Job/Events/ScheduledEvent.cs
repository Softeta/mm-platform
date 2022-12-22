using Domain.Seedwork;
using System;

namespace Scheduler.Job.Events
{
    public abstract class ScheduledEvent : Event
    {
        public ScheduledEvent(DateTimeOffset emittedAt, string eventName)
        {
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
