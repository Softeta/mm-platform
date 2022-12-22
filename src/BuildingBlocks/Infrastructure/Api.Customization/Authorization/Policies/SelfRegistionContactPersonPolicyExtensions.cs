using API.Customization.Authentication.Constants;
using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Policies
{
    public static class SelfRegistionContactPersonPolicyExtensions
    {
        public static void AddSelfRegistrationContactPersonPolicy(this AuthorizationOptions authorizationOptions)
        {
            authorizationOptions.AddPolicy(CustomPolicies.IsAllowedContactPersonRegisterHimself, policyBuilder =>
            {
                policyBuilder.AddAuthenticationSchemes(AuthenticationSchemas.AzureAdB2CCompanies);
                policyBuilder.AddRequirements(new AllowRegisterContactPersonHimselfRequirement());
            });
        }
    }
}
