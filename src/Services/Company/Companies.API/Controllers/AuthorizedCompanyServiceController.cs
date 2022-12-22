using API.Customization.Authorization.Constants;
using API.Customization.Controllers;
using Domain.Seedwork.Exceptions;

namespace Companies.API
{
    public abstract class AuthorizedCompanyServiceController : AuthorizedApiController
    {
        protected void Validate(Guid? companyId = null, Guid? contactPersonId = null)
        {
            if (companyId.HasValue)
            {
                ValidateAccess(companyId.Value, CompanyId);
            }
            if (contactPersonId.HasValue)
            {
                ValidateAccess(contactPersonId.Value, ContactId);
            }
        }

        protected void ValidateContactUpdate(Guid? contactId = null)
        {
            if (CustomScopes.FrontOffice.Company == Scope)
            {
                if (!IsAdmin && contactId.HasValue)
                {
                    ValidateAccess(contactId.Value, CompanyId);
                }
            }
        }

        private void ValidateAccess(Guid requestedId, Guid? ownedId)
        {
            if (CustomScopes.FrontOffice.Company == Scope)
            {
                if (requestedId != ownedId)
                {
                    throw new ForbiddenException("Access denied", ErrorCodes.Forbidden.CompanyServiceAccessDenied);
                }
            }
        }

        protected void ValidateScope()
        {
            if (CustomScopes.FrontOffice.Company != Scope)
            {
                throw new ForbiddenException("Access denied", ErrorCodes.Forbidden.CompanyServiceAccessDenied);
            }
        }
    }
}
