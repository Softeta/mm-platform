using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;

namespace Persistence.Customization.FileStorage.Clients.Public
{
    public class PublicFileUploadClient : FileUploadClient, IPublicFileUploadClient
    {
        public PublicFileUploadClient(IOptions<PublicStorageAccountConfigurations> configurations)
            : base(configurations)
        {
        }
    }
}
