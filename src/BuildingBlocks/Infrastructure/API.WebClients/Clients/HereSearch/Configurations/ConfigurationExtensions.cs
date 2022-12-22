using API.WebClients.Clients.HereSearch.Models;
using Microsoft.Extensions.Configuration;

namespace API.WebClients.Clients.HereSearch.Configurations
{
    public static class ConfigurationExtensions
    {
        public static HereSearchApiOptions GetHereSearchApiOptions(this ConfigurationManager configuration)
        {
            return configuration.GetSection("HereSearchApi").Get<HereSearchApiOptions>();
        }
    }
}
