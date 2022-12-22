using Contracts.Job.Companies.Requests;
using Contracts.Shared;
using MediatR;
using CompanyJobDomain = Jobs.Domain.Aggregates.JobAggregate.ValueObjects.Company;

namespace Jobs.Application.Commands
{
    public record UpdateCompanyCommand(
        Guid JobId,
        Address? Address,
        string? Description,
        IEnumerable<JobContactPersonRequest> ContactPersonsToAdd,
        IEnumerable<Guid> ContactPersonsToRemove,
        Guid MainContactId
        ) : IRequest<CompanyJobDomain>;
}
