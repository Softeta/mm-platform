namespace EventBus.EventHandlers
{
    public class IntegrationEvent
    {
        protected IntegrationEvent(DateTimeOffset emittedAt)
        {
            EventId = Guid.NewGuid();
            EmittedAt = emittedAt;
        }

        protected IntegrationEvent()
        {
        }

        public Guid EventId { get; init; }
        public DateTimeOffset EmittedAt { get; init; }
    }
}
