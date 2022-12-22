using BackOffice.Shared.Entities;
using MediatR;
using System.Collections.Generic;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal record AddCachedUsersCommand(
        ICollection<BackOfficeUserEntity> BackOfficeUsers,
        ICollection<BackOfficeUserEntity> CachedUsers) : INotification;
}
