using API.Customization.Authorization.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.RequirementHandlers.BaseHandlers
{
    public abstract class IsBackOfficeUserHandler<T> : AuthorizationHandler<T> where T : IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            T requirement)
        {
            var isBackOfficeUser = context.User.HasClaim(x => x.Value == CustomScopes.BackOffice.User);

            if (!isBackOfficeUser)
            {
                return Task.CompletedTask;
            }

            var hasProperRolesAssigned = context.User.IsInRole(Roles.BackOffice.Researcher) ||
                                         context.User.IsInRole(Roles.BackOffice.Consultant) ||
                                         context.User.IsInRole(Roles.BackOffice.Admin);

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
