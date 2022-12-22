using MediatR;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public class UpdateImageCommandHandler<TDeleteClient, TUploadClient> : IRequestHandler<UpdateImageCommand<TDeleteClient, TUploadClient>, Guid>
        where TDeleteClient : IFileDeleteClient
        where TUploadClient : IFileUploadClient
    {
        private readonly IMediator _mediator;

        public UpdateImageCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Handle(UpdateImageCommand<TDeleteClient, TUploadClient> request, CancellationToken cancellationToken)
        {
            var deleteImageCommand = new DeleteFileCommand<TDeleteClient>(
                request.ContainerName, 
                request.TablePartitionKey,
                request.CacheId);
            await _mediator.Publish(deleteImageCommand, cancellationToken);

            var uploadImageCommand = new UploadImageCommand<TUploadClient>(
                request.File, 
                request.ContainerName, 
                request.TablePartitionKey,
                request.Category, 
                request.ExpiresInMinutes);
            return await _mediator.Send(uploadImageCommand, cancellationToken);
        }
    }
}
