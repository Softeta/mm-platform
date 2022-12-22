using Companies.Domain.Aggregates.CompanyAggregate;
using Contracts.Shared;
using Contracts.Shared.Requests;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public record UpdateCompanyCommand(
        Guid CompanyId, 
        string? WebsiteUrl,
        string? LinkedInUrl,
        string? GlassdoorUrl,
        AddressWithLocation Address,
        UpdateFileCacheRequest Logo,
        IEnumerable<Industry> Industries) : IRequest<Company>;
}
