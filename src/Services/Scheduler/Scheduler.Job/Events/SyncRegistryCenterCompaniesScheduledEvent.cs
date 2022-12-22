using EventBus.Constants;
using System;

namespace Scheduler.Job.Events
{
    public class SyncRegistryCenterCompaniesScheduledEvent : ScheduledEvent
    {
        public SyncRegistryCenterCompaniesScheduledEvent(DateTimeOffset emittedAt) 
            : base(emittedAt, Topics.SchedulerJobScheduled.Filters.SyncRegistryCenterCompaniesScheduled)
        {
        }
    }
}
