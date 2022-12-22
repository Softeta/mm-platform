namespace Persistence.Customization.FileStorage.Clients
{
    public interface IFileDeleteClient
    {
        Task<bool> DeleteAsync(string fileUrl, CancellationToken cancellationToken = default);
        Task BatchDeleteAsync(Uri[] blobUris, CancellationToken cancellationToken = default);
        Task DeleteBlobsFromFolderAsync(string containerName, string folderName, CancellationToken cancellationToken = default);
    }
}
