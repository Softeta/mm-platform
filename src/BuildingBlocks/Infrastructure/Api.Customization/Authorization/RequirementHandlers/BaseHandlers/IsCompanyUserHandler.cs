using API.Customization.Authorization.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.RequirementHandlers.BaseHandlers
{
    public abstract class IsCompanyUserHandler<T> : AuthorizationHandler<T> where T : IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            T requirement)
        {
            var isCompany = context.User.HasClaim(x => x.Value == CustomScopes.FrontOffice.Company);

            if (isCompany)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
