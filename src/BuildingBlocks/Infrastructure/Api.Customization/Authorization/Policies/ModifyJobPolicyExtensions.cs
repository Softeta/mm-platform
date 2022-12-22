using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ModifyJobPolicyExtensions
    {
        public static void AddModifyJobPolicy(this AuthorizationOptions authorizationOptions, params string[] schemes)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedModifyJob, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(schemes);
                policyBuilder.AddRequirements(new AllowModifyJobRequirement());
            });
        }
    }
}
