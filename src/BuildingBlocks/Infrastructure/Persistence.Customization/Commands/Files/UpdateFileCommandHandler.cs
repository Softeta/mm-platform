using MediatR;
using Persistence.Customization.FileStorage.Clients;

namespace Persistence.Customization.Commands.Files
{
    public class UpdateFileCommandHandler<TDeleteClient, TUploadClient> 
        : IRequestHandler<UpdateFileCommand<TDeleteClient, TUploadClient>, Guid> 
        where TDeleteClient : IFileDeleteClient
        where TUploadClient : IFileUploadClient
    {
        private readonly IMediator _mediator;

        public UpdateFileCommandHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Guid> Handle(UpdateFileCommand<TDeleteClient, TUploadClient> request, CancellationToken cancellationToken)
        {
            var deleteFileCommand = new DeleteFileCommand<TDeleteClient>(
                request.ContainerName,
                request.TablePartitionKey,
                request.CacheId);
            await _mediator.Publish(deleteFileCommand, cancellationToken);

            var uploadFileCommand = new UploadFileCommand<TUploadClient>(
                request.ContainerName,
                request.TablePartitionKey,
                request.Category,
                request.ExpiresInMinutes,
                request.File);
            return await _mediator.Send(uploadFileCommand, cancellationToken);
        }
    }
}
