using EventBus.Constants;
using System;

namespace Scheduler.Job.Events
{
    public class RemoveExpiredFileCacheScheduledEvent : ScheduledEvent
    {
        public RemoveExpiredFileCacheScheduledEvent(DateTimeOffset emittedAt) 
            : base(emittedAt, Topics.SchedulerJobScheduled.Filters.RemoveExpiredFileCacheScheduled)
        {
        }
    }
}
