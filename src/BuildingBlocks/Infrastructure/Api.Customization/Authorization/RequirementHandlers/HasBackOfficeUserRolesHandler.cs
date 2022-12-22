using API.Customization.Authorization.Constants;
using API.Customization.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.RequirementHandlers
{
    public class HasBackOfficeUserRolesHandler : AuthorizationHandler<BackOfficeUserWithRolesRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            BackOfficeUserWithRolesRequirement requirement)
        {
            var isBackOfficeUser = context.User.HasClaim(x => x.Value == CustomScopes.BackOffice.User);

            if (!isBackOfficeUser)
            {
                return Task.CompletedTask;
            }

            var hasProperRolesAssigned = requirement.Roles.Any(x => context.User.IsInRole(x));

            if (hasProperRolesAssigned)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
