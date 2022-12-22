using EventBus.EventHandlers;
using System;
using BackOffice.Shared.Entities;

namespace BackOffice.Users.CacheRefresher.EventBus
{
    internal class BackOfficeUserChangedIntegrationEvent : IntegrationEvent
    {
        public BackOfficeUserChangedIntegrationEvent(UserPayload payload) : base(DateTimeOffset.UtcNow)
        {
            Payload = payload;
        }

        public UserPayload Payload { get; }
    }

    internal class UserPayload
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PictureUri { get; set; }

        public static UserPayload FromEntity(BackOfficeUserEntity entity)
        {
            return new UserPayload
            {
                UserId = Guid.Parse(entity.RowKey),
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                PictureUri = entity.PictureUri
            };
        }
    }
}
