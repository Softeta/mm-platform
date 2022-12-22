using API.Customization.Authorization.RequirementHandlers.BaseHandlers;
using API.Customization.Authorization.Requirements;

namespace API.Customization.Authorization.RequirementHandlers
{
    internal class ContactPersonCanReadPublicCompanyHandler : IsCompanyUserHandler<AllowReadPublicCompanyRequirement>
    {
    }
}
