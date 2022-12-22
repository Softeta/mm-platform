using MediatR;

namespace Companies.Application.Commands.ContactPersons
{
    public record DeleteContactPersonAssetsCommand(
        Guid? ExternalId,
        string? PictureOriginalUri,
        string? PictureThumbnailUri
        ) : INotification;
}
