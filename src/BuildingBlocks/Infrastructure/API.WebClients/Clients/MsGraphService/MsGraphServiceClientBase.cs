using API.Customization.Authentication;
using Azure.Identity;
using Microsoft.Extensions.Options;
using Microsoft.Graph;

namespace API.WebClients.Clients.MsGraphService
{
    public abstract class MsGraphServiceClientBase
    {
        private readonly AppRegistrationSettings _configurations;

        public MsGraphServiceClientBase(IOptions<AppRegistrationSettings> options)
        {
            _configurations = options.Value;
        }

        protected GraphServiceClient GetGraphClient()
        {
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };

            var clientSecretCredentials = new ClientSecretCredential(
                _configurations.TenantId,
                _configurations.ClientId,
                _configurations.ClientSecret,
                options);

            var scopes = new[] { "https://graph.microsoft.com/.default" };

            return new GraphServiceClient(clientSecretCredentials, scopes);
        }
    }
}
