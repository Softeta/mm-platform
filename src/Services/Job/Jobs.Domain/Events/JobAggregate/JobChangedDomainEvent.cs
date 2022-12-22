using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Domain.Events.JobAggregate
{
    public abstract class JobChangedDomainEvent : Event
    {
        protected JobChangedDomainEvent(Job job, DateTimeOffset emittedAt, string eventName)
        {
            Job = job;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public Job Job { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
