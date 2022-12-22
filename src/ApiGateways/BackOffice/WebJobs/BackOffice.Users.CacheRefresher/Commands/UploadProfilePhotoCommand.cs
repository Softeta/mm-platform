using BackOffice.Shared.Entities;
using BackOffice.Users.CacheRefresher.BackOfficeUsersServices;
using MediatR;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal record UploadProfilePhotoCommand(BackOfficeUserEntity CachedUser, ProfilePhoto ProfilePhoto) : INotification;
}
