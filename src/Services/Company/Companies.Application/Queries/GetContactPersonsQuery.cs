using Contracts.Company.Responses.ContactPersons;
using MediatR;

namespace Companies.Application.Queries
{
    public record GetContactPersonsQuery(
        Guid companyId,
        IEnumerable<Guid>? ContactPersons,
        int PageNumber,
        int PageSize) : IRequest<GetContactPersonsResponse>;
}
