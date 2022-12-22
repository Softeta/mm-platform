using Candidates.Domain.Aggregates.CandidateAggregate;

namespace Candidates.Application.Commands.WorkExperiences
{
    public record DeleteCandidateWorkExperienceCommand(
        Guid CandidateId,
        Guid WorkExperienceId,
        Guid UserId,
        string Scope
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
