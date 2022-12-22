using API.Customization.Authentication.Constants;
using Microsoft.Identity.Web;
using System.Security.Claims;

namespace API.Customization.HttpContexts
{
    public static class HttpContextExtensions
    {
        private const string NoAuthScope = "NoAuthScope";
        private const string EmailsClaim = "emails";
        private const string CompanyIdClaim = "extension_CompanyId";
        private const string ContactIdClaim = "extension_ContactId";
        private const string IsAdminClaim = "extension_IsAdmin";

        public static Guid UserId(this ClaimsPrincipal user)
        {
            Guid.TryParse(user.Claims.FirstOrDefault(c => c.Type == ClaimConstants.ObjectId)?.Value, out var userId);

            return userId;
        }

        public static Guid? CompanyId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.Claims.FirstOrDefault(c => c.Type == CompanyIdClaim)?.Value, out var companyId))
            {
                return companyId;
            };

            return null;
        }

        public static Guid? ContactId(this ClaimsPrincipal user)
        {
            if (Guid.TryParse(user.Claims.FirstOrDefault(c => c.Type == ContactIdClaim)?.Value, out var contactId))
            {
                return contactId;
            }

            return null;
        }

        public static string? Email(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(c => c.Type == EmailsClaim)?.Value;
        }

        public static string Scope(this ClaimsPrincipal user)
        {
            var result = user.Claims.FirstOrDefault(c => c.Type == ClaimConstants.Scope)?.Value;
            return result ?? NoAuthScope;
        }

        public static string? AuthenticationSchema(this ClaimsPrincipal user)
        {
            AuthenticationSchemas.ScopeToSchemas.TryGetValue(user.Scope(), out var schema);

            return schema;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return bool.TryParse(user.Claims.FirstOrDefault(c => c.Type == IsAdminClaim)?.Value, out var isAdmin) && isAdmin;
        }
    }
}
