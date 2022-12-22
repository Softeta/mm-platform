using Newtonsoft.Json;

namespace Candidates.Infrastructure.Clients.Talogy.Models.Token
{
    public class AuthenticationResult
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = null!;

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; } = null!;

        public DateTimeOffset? ExpiresOn { get; set; }

        public void CalculateExpirationDate()
        {
            ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(ExpiresIn);
        }
    }
}
