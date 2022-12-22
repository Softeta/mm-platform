using Companies.Infrastructure.Clients;
using MediatR;
using Persistence.Customization.Commands.Files;
using Persistence.Customization.FileStorage.Clients.Public;

namespace Companies.Application.Commands.ContactPersons
{
    public class DeleteContactPersonAssetsCommandHandler : INotificationHandler<DeleteContactPersonAssetsCommand>
    {
        private readonly IMsGraphServiceClient _msGraphServiceClient;
        private readonly IMediator _mediator;

        public DeleteContactPersonAssetsCommandHandler(
            IMsGraphServiceClient msGraphServiceClient,
            IMediator mediator)
        {
            _msGraphServiceClient = msGraphServiceClient;
            _mediator = mediator;
        }

        public async Task Handle(DeleteContactPersonAssetsCommand notification, CancellationToken cancellationToken)
        {
            await DeleteUserFromAuthProviderAsync(notification.ExternalId);
            
            await DeleteAllPublicContactPersonFilesAsync(
                notification.PictureOriginalUri, 
                notification.PictureThumbnailUri);
        }

        private async Task DeleteUserFromAuthProviderAsync(Guid? externalId)
        {
            if (externalId is null)
            {
                return;
            }

            await _msGraphServiceClient.DeleteUserAsync(externalId.Value);
        }

        private async Task DeleteAllPublicContactPersonFilesAsync(
            string? pictureOriginalUri,
            string? pictureThumbnailUri)
        {
            var uris = new List<Uri>();

            if (!string.IsNullOrWhiteSpace(pictureOriginalUri))
            {
                uris.Add(new Uri(pictureOriginalUri));
            }
            if (!string.IsNullOrWhiteSpace(pictureThumbnailUri))
            {
                uris.Add(new Uri(pictureThumbnailUri));
            }
            var command = new DeleteFilesBatchCommand<IPublicFileDeleteClient>(uris);
            await _mediator.Publish(command);
        }
    }
}
