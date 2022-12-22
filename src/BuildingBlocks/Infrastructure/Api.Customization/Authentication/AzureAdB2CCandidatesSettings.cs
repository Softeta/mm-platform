namespace API.Customization.Authentication
{
    public class AzureAdB2CCandidatesSettings
    {
        public string Instance { get; set; } = null!;
        public string Domain { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string SignedOutCallbackPath { get; set; } = null!;
        public string SignUpSignInPolicyId { get; set; } = null!;
        public string Scope { get; set; } = null!;
    }
}
