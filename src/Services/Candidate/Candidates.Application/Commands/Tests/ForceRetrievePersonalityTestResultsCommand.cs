using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using MediatR;

namespace Candidates.Application.Commands.Tests
{
    public record ForceRetrievePersonalityTestResultsCommand(
        Guid CandidateId,
        string PackageInstanceId) 
        : IRequest<CandidateTests?>;
}
