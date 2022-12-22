using API.Customization.Authorization.Constants;

namespace API.Customization.Authentication.Constants
{
    public static class AuthenticationSchemas
    {
        public const string AzureAd = "AzureAd";
        public const string AzureAdB2CCandidates = "AzureAdB2CCandidates";
        public const string AzureAdB2CCompanies = "AzureAdB2CCompanies";

        public static Dictionary<string, string> ScopeToSchemas = new()
        {
            { CustomScopes.BackOffice.User, AzureAd },
            { CustomScopes.FrontOffice.Candidate, AzureAdB2CCandidates },
            { CustomScopes.FrontOffice.Company, AzureAdB2CCompanies }
        };
    }
}
