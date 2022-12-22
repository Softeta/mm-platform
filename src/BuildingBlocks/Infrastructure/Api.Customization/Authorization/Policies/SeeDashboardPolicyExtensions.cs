using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class SeeDashboardPolicyExtensions
    {
        public static void AddSeeDashboardPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.CanSeeDashboard, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAd);
                policyBuilder.AddRequirements(new BackOfficeUserWithRolesRequirement(
                    new[]
                    {
                        Roles.BackOffice.Researcher,
                        Roles.BackOffice.Consultant
                    }));
            });
        }
    }
}
