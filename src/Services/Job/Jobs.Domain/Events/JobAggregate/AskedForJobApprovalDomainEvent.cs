using Domain.Seedwork;

namespace Jobs.Domain.Events.JobAggregate
{
    public class AskedForJobApprovalDomainEvent : Event
    {
        public Guid JobId { get; set; }
        public Guid RouteKey { get; set; }
        public string ReceiverEmail { get; set; } = null!;
        public DateTimeOffset ShareDate { get; set; }
    }
}
