using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Private;

public class PrivateFileDownloadClient : FileDownloadClient, IPrivateFileDownloadClient
{
    public PrivateFileDownloadClient(IOptions<PrivateStorageAccountConfigurations> configurations)
        : base(configurations)
    {
    }
}