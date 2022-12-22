using Microsoft.AspNetCore.Authorization;

namespace API.Customization.Authorization.Requirements
{
    public class BackOfficeUserWithRolesRequirement : IAuthorizationRequirement
    {
        public string[] Roles { get; init; }

        public BackOfficeUserWithRolesRequirement(string[] roles)
        {
            Roles = roles;
        }
    }
}
