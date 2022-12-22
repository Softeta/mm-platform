using EventBus.Constants;
using System;

namespace Scheduler.Job.Events
{
    public class DeleteCandidatesScheduledEvent : ScheduledEvent
    {
        public DeleteCandidatesScheduledEvent(DateTimeOffset emittedAt) 
            : base(emittedAt, Topics.SchedulerJobScheduled.Filters.DeleteCandidatesScheduled)
        {
        }
    }
}
