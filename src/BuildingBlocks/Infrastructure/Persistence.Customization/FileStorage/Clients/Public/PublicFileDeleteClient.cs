using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Public
{
    public class PublicFileDeleteClient : FileDeleteClient, IPublicFileDeleteClient
    {
        public PublicFileDeleteClient(IOptions<PublicStorageAccountConfigurations> configurations) 
            : base(configurations)
        {
        }
    }
}
