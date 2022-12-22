using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class ModifyCompanyPolicyExtensions
    {
        public static void AddModifyCompanyPolicy(this AuthorizationOptions authorizationOptions, params string[] schemes)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedModifyCompany, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(schemes);
                policyBuilder.AddRequirements(new AllowModifyCompanyRequirement());
            });
        }
    }
}
