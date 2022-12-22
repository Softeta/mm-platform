using BackOffice.Shared.Entities;
using MediatR;
using System.Collections.Generic;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal record UpdateCachedUsersCommand(
        ICollection<BackOfficeUserEntity> BackOfficeUsers,
        ICollection<BackOfficeUserEntity> CachedUsers) : INotification;
}
