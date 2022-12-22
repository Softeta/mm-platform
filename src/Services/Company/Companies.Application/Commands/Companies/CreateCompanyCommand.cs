using Companies.Domain.Aggregates.CompanyAggregate;
using Contracts.Shared;
using Contracts.Shared.Requests;
using Domain.Seedwork.Enums;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public record CreateCompanyCommand(
        string Name, 
        string RegistrationNumber,
        string? WebsiteUrl,
        string? LinkedInUrl,
        string? GlassdoorUrl,
        AddressWithLocation Address,
        AddFileCacheRequest? Logo,
        string PersonEmail,
        string PersonFirstName,
        string PersonLastName,
        string? PersonPhoneCountryCode,
        string? PersonPhoneNumber,
        Position? PersonPosition,
        AddFileCacheRequest? PersonPicture,
        IEnumerable<Industry> Industries,
        Guid CreatedById,
        Scope CreatedByScope) : IRequest<Company>;
}
