using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Private
{
    public class PrivateFileUploadClient : FileUploadClient, IPrivateFileUploadClient
    {
        public PrivateFileUploadClient(IOptions<PrivateStorageAccountConfigurations> configurations) 
            : base(configurations)
        {
        }
    }
}
