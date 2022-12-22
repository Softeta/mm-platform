using Azure;
using Azure.Data.Tables;
using Domain.Seedwork.Exceptions;
using MediatR;
using Persistence.Customization.FileStorage.Clients;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Clients;
using Persistence.Customization.TableStorage.Models;
using System.Net;

namespace Persistence.Customization.Commands.Files
{
    public class DeleteFileCommandHandler<TDeleteClient> : INotificationHandler<DeleteFileCommand<TDeleteClient>> 
        where TDeleteClient : IFileDeleteClient
    {
        private readonly TDeleteClient _fileDeleteClient;
        private readonly TableClient _fileCacheTableClient;

        public DeleteFileCommandHandler(
            TDeleteClient fileDeleteClient,
            IPrivateTableServiceClient privateTableServiceClient)
        {
            _fileDeleteClient = fileDeleteClient;
            _fileCacheTableClient = privateTableServiceClient.GetTableClient(FileCacheTableStorage.TableName);
        }

        public async Task Handle(DeleteFileCommand<TDeleteClient> request, CancellationToken cancellationToken)
        {
            try
            {
                await _fileCacheTableClient.GetEntityAsync<FileCacheEntity>(
                    request.TablePartitionKey,
                    request.CacheId.ToString(),
                    cancellationToken: cancellationToken);
            }
            catch (RequestFailedException ex)
            {
                if (ex.Status == (int)HttpStatusCode.NotFound)
                {
                    throw new NotFoundException($"File not found. CacheId: {request.CacheId}",
                        ErrorCodes.NotFound.FileNotFound);
                }
                throw ex;
            }

            await _fileDeleteClient.DeleteBlobsFromFolderAsync(
                request.ContainerName,
                request.CacheId.ToString(),
                cancellationToken);

            await _fileCacheTableClient.DeleteEntityAsync(
                request.TablePartitionKey,
                request.CacheId.ToString(),
                cancellationToken: cancellationToken);
        }
    }
}
