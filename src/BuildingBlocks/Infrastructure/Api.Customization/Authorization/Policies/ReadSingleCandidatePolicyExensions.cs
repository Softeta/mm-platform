using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ReadSingleCandidatePolicyExensions
    {
        public static void AddReadSingleCandidatePolicy(this AuthorizationOptions authorizationOptions, params string[] schemes)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedReadSingleCandidate, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(schemes);
                policyBuilder.AddRequirements(new AllowReadSingleCandidateRequirement());
            });
        }
    }
}
