using BackOffice.Shared.Entities;
using MediatR;
using System.Collections.Generic;

namespace BackOffice.Users.CacheRefresher.EventHandlers
{
    internal record BackOfficeUsersUpdatedEvent(ICollection<BackOfficeUserEntity> Users) : INotification;
}
