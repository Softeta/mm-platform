using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Private
{
    public class PrivateBlobClient : IPrivateBlobClient
    {
        private readonly PrivateStorageAccountConfigurations _configurations;

        public PrivateBlobClient(IOptions<PrivateStorageAccountConfigurations> configurations)
        {
            _configurations = configurations.Value;
        }

        public Uri GetSignedUri(string blobUri)
        {
            var credentials = new StorageSharedKeyCredential(_configurations.AccountName, _configurations.AccountKey);
            var blobClient = new BlobClient(new Uri(blobUri), credentials);

            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_configurations.ExpirationInMinutes);

            return blobClient.GenerateSasUri(BlobSasPermissions.Read,expiresAt);
        }
    }
}
