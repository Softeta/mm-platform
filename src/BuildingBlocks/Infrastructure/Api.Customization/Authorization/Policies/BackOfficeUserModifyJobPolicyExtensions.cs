using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class BackOfficeUserModifyJobPolicyExtensions
    {
        public static void AddBackOfficeUserModifyJobPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedBackOfficeUserModifyJob, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAd);
                policyBuilder.AddRequirements(new BackOfficeUserRequirement());
            });
        }
    }
}
