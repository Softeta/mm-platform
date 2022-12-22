using MediatR;
using Microsoft.Extensions.Logging;
using Persistence.Customization.FileStorage.Clients.Public;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal class DeleteProfilePhotoCommandHandler : INotificationHandler<DeleteProfilePhotoCommand>
    {
        private readonly ILogger<DeleteProfilePhotoCommandHandler> _logger;
        private readonly IPublicFileDeleteClient _fileDeleteClient;

        public DeleteProfilePhotoCommandHandler(
            ILogger<DeleteProfilePhotoCommandHandler> logger,
            IPublicFileDeleteClient fileDeleteClient)
        {
            _fileDeleteClient = fileDeleteClient;
            _logger = logger;
        }

        public async Task Handle(DeleteProfilePhotoCommand notification, CancellationToken cancellationToken)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(notification.PictureUri) &&
                    !await _fileDeleteClient.DeleteAsync(notification.PictureUri, cancellationToken))
                {
                    _logger.LogCritical("Failed trying delete back-office user profile picture {URI}", notification.PictureUri);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed trying delete back-office user profile picture {URI}", notification.PictureUri);
            }
        }
    }
}
