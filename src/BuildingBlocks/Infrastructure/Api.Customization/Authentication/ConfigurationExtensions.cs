using API.Customization.Authentication.Constants;
using Microsoft.Extensions.Configuration;

namespace API.Customization.Authentication
{
    public static class ConfigurationExtensions
    {
        public static AzureAdSettings GetAzureAdSettings(this ConfigurationManager configuration)
        {
            return configuration.GetSection(AuthenticationSchemas.AzureAd).Get<AzureAdSettings>();
        }

        public static AzureAdB2CCandidatesSettings GetAzureAdB2CCandidatesSettings(this ConfigurationManager configuration)
        {
            return configuration.GetSection(AuthenticationSchemas.AzureAdB2CCandidates).Get<AzureAdB2CCandidatesSettings>();
        }
    }
}
