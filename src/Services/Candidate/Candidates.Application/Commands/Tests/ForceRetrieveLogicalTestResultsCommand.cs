using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record ForceRetrieveLogicalTestResultsCommand(
        Guid CandidateId,
        string PackageInstanceId) 
        : IRequest<CandidateTests?>;
}
