using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients;

public class FileDownloadClient : IFileDownloadClient
{
    private readonly StorageAccountConfigurations _configurations;

    protected FileDownloadClient(IOptions<StorageAccountConfigurations> configurations)
    {
        _configurations = configurations.Value;
    }

    public async Task<Stream> DownloadAsync(string url, CancellationToken ct = default)
    {
        var storageCredential = new StorageSharedKeyCredential(_configurations.AccountName, _configurations.AccountKey);
        var blobClient = new BlobClient(new Uri(url), storageCredential);

        var stream = new MemoryStream();
        await blobClient.DownloadToAsync(stream, ct);
        stream.Position = 0;

        return stream;
    }
}