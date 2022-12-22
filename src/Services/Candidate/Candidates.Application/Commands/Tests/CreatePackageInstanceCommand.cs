using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Clients.Talogy.Models.Responses;
using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record CreatePackageInstanceCommand(Candidate candidate, string PackageInstanceId, string PackageTypeId) : IRequest<CreatedPackageInstance>;
}