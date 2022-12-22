namespace Persistence.Customization.Storages
{
    public class StorageAccountConfigurations
    {
        public string AccountName { get; set; } = null!;
        public string AccountKey { get; set; } = null!;
        public string FileStorageUrl => $"https://{AccountName}.blob.core.windows.net/";
        public string ConnectionString => $"DefaultEndpointsProtocol=https;AccountName={AccountName};AccountKey={AccountKey};EndpointSuffix=core.windows.net";
    }
}
