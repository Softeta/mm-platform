namespace API.Customization.Authentication
{
    public class AppRegistrationSettings
    {
        public string TenantId { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
    }
}
