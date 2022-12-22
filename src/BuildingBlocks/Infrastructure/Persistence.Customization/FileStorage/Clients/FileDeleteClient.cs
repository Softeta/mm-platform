using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients
{
    public abstract  class FileDeleteClient : IFileDeleteClient
    {
        private readonly StorageAccountConfigurations _configurations;

        protected FileDeleteClient(IOptions<StorageAccountConfigurations> configurations)
        {
            _configurations = configurations.Value;
        }

        public async Task BatchDeleteAsync(Uri[] blobUris, CancellationToken cancellationToken = default)
        {
            var serviceClient = new BlobServiceClient(_configurations.ConnectionString);
            var blobBatchClient = serviceClient.GetBlobBatchClient();

            await blobBatchClient.DeleteBlobsAsync(blobUris, DeleteSnapshotsOption.IncludeSnapshots, cancellationToken);
        }

        public async Task<bool> DeleteAsync(string fileUrl, CancellationToken cancellationToken = default)
        {
            var storageCredential = new StorageSharedKeyCredential(_configurations.AccountName, _configurations.AccountKey);
            var blobClient = new BlobClient(new Uri(fileUrl), storageCredential);

            return await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots, cancellationToken: cancellationToken);
        }

        public async Task DeleteBlobsFromFolderAsync(string containerName, string folderName, CancellationToken cancellationToken = default)
        {
            var storageCredential = new StorageSharedKeyCredential(_configurations.AccountName, _configurations.AccountKey);
            var blobContainer = new BlobServiceClient(new Uri(_configurations.FileStorageUrl), storageCredential)
                .GetBlobContainerClient(containerName);
           
            var blobItems = blobContainer.GetBlobsAsync(prefix: folderName);

            await foreach (var blobItem in blobItems)
            {
                var blobClient = blobContainer.GetBlobClient(blobItem.Name);
                await blobClient.DeleteIfExistsAsync();
            }
        }
    }
}
