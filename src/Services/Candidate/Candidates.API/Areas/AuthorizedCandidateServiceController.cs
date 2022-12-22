using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using Domain.Seedwork.Exceptions;

namespace Candidates.API.Areas
{
    public abstract class AuthorizedCandidateServiceController : AuthorizedApiController
    {
        protected void Validate(Guid? externalId = null)
        {
            ValidateAccess(externalId, UserId);
        }

        private void ValidateAccess(Guid? requestedId, Guid? ownedId)
        {
            if (CustomScopes.FrontOffice.Candidate == Scope)
            {
                if (requestedId is null || requestedId.Value != ownedId)
                {
                    throw new ForbiddenException("Access denied", ErrorCodes.Forbidden.CandidateServiceAccessDenied);
                }
            }
        }
    }
}
