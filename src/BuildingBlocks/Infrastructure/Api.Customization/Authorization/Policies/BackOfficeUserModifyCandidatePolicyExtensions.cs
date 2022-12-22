using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class BackOfficeUserModifyCandidatePolicyExtensions
    {
        public static void AddBackOfficeUserModifyCandidatePolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedBackOfficeUserModifyCandidate, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAd);
                policyBuilder.AddRequirements(new BackOfficeUserRequirement());
            });
        }
    }
}
