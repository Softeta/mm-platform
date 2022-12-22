using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands.Educations
{
    public record DeleteCandidateEducationCommand(
        Guid CandidateId,
        Guid EducationId,
        Guid UserId,
        string Scope
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
