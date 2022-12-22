using MediatR;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal record DeleteProfilePhotoCommand(string? PictureUri) : INotification;
}
