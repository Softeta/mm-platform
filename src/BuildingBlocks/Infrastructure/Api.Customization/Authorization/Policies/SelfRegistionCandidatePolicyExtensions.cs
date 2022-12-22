using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class SelfRegistionCandidatePolicyExtensions
    {
        public static void AddSelfModificationCandidatePolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedCandidateModifyHimself, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAdB2CCandidates);
                policyBuilder.AddRequirements(new AllowRegisterCandidateHimselfRequirement());
            });
        }
    }
}
