using Microsoft.Extensions.Configuration;

namespace API.WebClients.Clients.DanishCompaniesService.Configurations
{
    public static class ConfigurationExtensions
    {
        public static DanishCrvApiOptions GetDanishCrvApiOptions(this ConfigurationManager configuration)
        {
            return configuration.GetSection("DanishCrvApi").Get<DanishCrvApiOptions>();
        }
    }
}
