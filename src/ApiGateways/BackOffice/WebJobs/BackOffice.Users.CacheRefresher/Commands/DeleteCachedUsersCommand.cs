using BackOffice.Shared.Entities;
using MediatR;
using System.Collections.Generic;

namespace BackOffice.Users.CacheRefresher.Commands
{
    public record DeleteCachedUsersCommand(ICollection<BackOfficeUserEntity> Users) : INotification;
}
