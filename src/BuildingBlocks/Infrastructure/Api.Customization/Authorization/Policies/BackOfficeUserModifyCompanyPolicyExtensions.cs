using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class BackOfficeUserModifyCompanyPolicyExtensions
    {
        public static void AddBackOfficeUserModifyCompanyPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedBackOfficeUserModifyCompany, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAd);
                policyBuilder.AddRequirements(new BackOfficeUserRequirement());
            });
        }
    }
}
