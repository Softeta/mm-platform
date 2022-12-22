using Candidates.Domain.Aggregates.CandidateTestsAggregate;

namespace Candidates.Application.Commands.Tests
{
    public record CreatePersonalityTestCommand(
        Guid CandidateId,
        Guid UserId,
        string Scope) 
        : ModifyCandidateBaseCommand<CandidateTests?>(CandidateId, UserId, Scope);
}
