using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Private
{
    public class PrivateFileDeleteClient : FileDeleteClient, IPrivateFileDeleteClient
    {
        public PrivateFileDeleteClient(IOptions<PrivateStorageAccountConfigurations> configurations) 
            : base(configurations)
        {
        }
    }
}
