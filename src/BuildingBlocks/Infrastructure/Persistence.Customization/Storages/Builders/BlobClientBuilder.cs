using Azure.Storage.Blobs;

namespace Persistence.Customization.Storages.Builders
{
    public class BlobClientBuilder
    {
        private string _connectionString = null!;
        private string _container = null!;
        private string _fileName = null!;

        public BlobClientBuilder ForStorageAccount(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public BlobClientBuilder WithContainer(string containerName)
        {
            _container = containerName;
            return this;
        }

        public BlobClientBuilder WithFileName(string fileName)
        {
            _fileName = fileName;
            return this;
        }

        public BlobClient Build()
        {
            return new BlobClient(_connectionString, _container, _fileName);
        }
    }
}
