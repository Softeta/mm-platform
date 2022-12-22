using Azure.Data.Tables;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Entities;
using BackOffice.Users.CacheRefresher.Constants;
using BackOffice.Users.CacheRefresher.EventHandlers;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence.Customization.FileStorage.Clients.Public;
using Persistence.Customization.FileStorage.Constants;
using Persistence.Customization.TableStorage.Clients;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BackOffice.Users.CacheRefresher.Commands
{
    internal class UploadProfilePhotoCommandHandler : INotificationHandler<UploadProfilePhotoCommand>
    {
        private readonly ILogger<UploadProfilePhotoCommandHandler> _logger;
        private readonly IPublicFileUploadClient _fileUploadClient;
        private readonly TableClient _backOfficeUsersClient;
        private readonly IMediator _mediator;
        private readonly string _userPicturesContainer;

        public UploadProfilePhotoCommandHandler(
            ILogger<UploadProfilePhotoCommandHandler> logger,
            IPublicFileUploadClient fileUploadClient,
            IWebTableServiceClient client,
            IConfiguration configuration,
            IMediator mediator)
        {
            _fileUploadClient = fileUploadClient;
            _mediator = mediator;
            _logger = logger;
            _backOfficeUsersClient = _backOfficeUsersClient = client.GetTableClient(TableStorageConstants.BackOfficeUsersTable);
            _userPicturesContainer = configuration[AppSettingNames.UserPicturesContainer];
        }

        public async Task Handle(UploadProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await using var memoryStream = new MemoryStream(request.ProfilePhoto.Photo);
                request.CachedUser.PictureUri = await _fileUploadClient.ExecuteAsync(memoryStream, FileExtensions.ImageExtension, _userPicturesContainer, cancellationToken);
                request.CachedUser.PictureETag = request.ProfilePhoto.ETag;

                await _backOfficeUsersClient.UpdateEntityAsync(request.CachedUser, request.CachedUser.ETag, cancellationToken: cancellationToken);
                await _mediator.Publish(new BackOfficeUsersUpdatedEvent(new Collection<BackOfficeUserEntity> { request.CachedUser }), cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Failed to upload new profile picture {UserId}", request.CachedUser.RowKey);
            }
        }
    }
}
