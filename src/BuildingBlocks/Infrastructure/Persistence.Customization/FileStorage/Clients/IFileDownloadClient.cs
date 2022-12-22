namespace Persistence.Customization.FileStorage.Clients;

public interface IFileDownloadClient
{
    Task<Stream> DownloadAsync(string url, CancellationToken ct = default);
}