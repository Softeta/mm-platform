using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ReadPublicCompanyPolicyExtensions
    {
        public static void AddReadPublicCompanyPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedReadPublicCompany, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies);
                policyBuilder.AddRequirements(new AllowReadPublicCompanyRequirement());
            });
        }
    }
}
