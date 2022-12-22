using EventBus.EventHandlers;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.BackOfficeUsers
{
    public class BackOfficeUserUpdatedIntegrationEvent : IntegrationEvent
    {
        public UserPayload? Payload { get; set; }
    }

    public class UserPayload
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }
    }
}
