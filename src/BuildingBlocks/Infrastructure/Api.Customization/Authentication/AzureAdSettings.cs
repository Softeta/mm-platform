namespace API.Customization.Authentication
{
    public class AzureAdSettings
    {
        public string Authority { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string AuthorizationUri { get; set; } = null!;
        public string TokenUri { get; set; } = null!;
        public string ApiScope { get; set; } = null!;
        public string ApplicationId { get; set; } = null!;
    }
}
