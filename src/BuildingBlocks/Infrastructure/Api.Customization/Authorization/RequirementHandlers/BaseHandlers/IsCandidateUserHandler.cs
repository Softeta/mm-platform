using API.Customization.Authorization.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.RequirementHandlers.BaseHandlers
{
    public abstract class IsCandidateUserHandler<T> : AuthorizationHandler<T> where T : IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(
           AuthorizationHandlerContext context,
           T requirement)
        {
            var isCandidate = context.User.HasClaim(x => x.Value == CustomScopes.FrontOffice.Candidate);

            if (isCandidate)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
