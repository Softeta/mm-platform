using Companies.Domain.Aggregates.CompanyAggregate;
using Contracts.Shared;
using MediatR;

namespace Companies.Application.Commands.Companies
{
    public record RegisterCompanyCommand(
        string Name, 
        string RegistrationNumber,
        AddressWithLocation Address,
        string PersonEmail,
        string PersonFirstName,
        string PersonLastName,
        string? PersonPhoneCountryCode,
        string? PersonPhoneNumber,
        Position? PersonPosition,
        IEnumerable<Industry> Industries) : IRequest<Company>;
}
