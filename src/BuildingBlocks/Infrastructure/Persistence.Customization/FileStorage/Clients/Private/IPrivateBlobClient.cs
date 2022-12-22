namespace Persistence.Customization.FileStorage.Clients.Private
{
    public interface IPrivateBlobClient
    {
        public Uri GetSignedUri(string blobUri);
    }
}
