namespace Candidates.Infrastructure.Clients.Talogy.Configurations
{
    public class TalogyConfigurations
    {
        public string AuthUrl { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;

        public string GrantType = "client_credentials";
    }
}
