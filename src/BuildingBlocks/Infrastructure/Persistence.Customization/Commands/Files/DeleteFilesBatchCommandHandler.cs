using Azure;
using MediatR;
using Persistence.Customization.FileStorage.Clients;
using System.Net;

namespace Persistence.Customization.Commands.Files
{
    public class DeleteFilesBatchCommandHandler<TDeleteClient> : INotificationHandler<DeleteFilesBatchCommand<TDeleteClient>>
        where TDeleteClient : IFileDeleteClient
    {
        private readonly TDeleteClient _fileDeleteClient;

        public DeleteFilesBatchCommandHandler(TDeleteClient fileDeleteClient)
        {
            _fileDeleteClient = fileDeleteClient;
        }

        public async Task Handle(DeleteFilesBatchCommand<TDeleteClient> notification, CancellationToken cancellationToken)
        {
            if (notification.Uris.Count > 0)
            {
                try
                {
                    await _fileDeleteClient.BatchDeleteAsync(notification.Uris.ToArray());
                }
                catch (AggregateException ex)
                {
                    HandleAggregateException(ex);
                }
            }
        }

        private static void HandleAggregateException(AggregateException ex)
        {
            foreach (RequestFailedException innerException in ex.InnerExceptions)
            {
                if (innerException.Status is (int)HttpStatusCode.NotFound)
                {
                    continue;
                }
                throw ex;
            }
        }
    }
}
