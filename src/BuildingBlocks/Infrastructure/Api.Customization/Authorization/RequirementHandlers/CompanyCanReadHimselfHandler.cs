﻿using API.Customization.Authorization.RequirementHandlers.BaseHandlers;
using API.Customization.Authorization.Requirements;

namespace API.Customization.Authorization.RequirementHandlers
{
    public class CompanyCanReadHimselfHandler : IsCompanyUserHandler<AllowReadCompanyRequirement>
    {
    }
}
