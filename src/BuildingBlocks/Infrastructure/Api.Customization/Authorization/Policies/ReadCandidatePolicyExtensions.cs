using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ReadCandidatePolicyExtensions
    {
        public static void AddReadCandidatePolicy(this AuthorizationOptions authorizationOptions, params string[] schemes)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedReadCandidate, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(schemes);
                policyBuilder.AddRequirements(new AllowReadCandidateRequirement());
            });
        }
    }
}
