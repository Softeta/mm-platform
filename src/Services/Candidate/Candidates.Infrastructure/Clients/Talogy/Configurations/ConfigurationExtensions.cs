using Microsoft.Extensions.Configuration;

namespace Candidates.Infrastructure.Clients.Talogy.Configurations
{
    public static class ConfigurationExtensions
    {
        public static TalogyConfigurations GetTalogyApiOptions(this ConfigurationManager configuration)
        {
            return configuration.GetSection("TalogyApi").Get<TalogyConfigurations>();
        }
    }
}
