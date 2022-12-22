using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ModifyCandidatePolicyExtensions
    {
        public static void AddModifyCandidatePolicy(this AuthorizationOptions authorizationOptions, params string[] schemes)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedModifyCandidate, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(schemes);
                policyBuilder.AddRequirements(new AllowModifyCandidateRequirement());
            });
        }
    }
}
