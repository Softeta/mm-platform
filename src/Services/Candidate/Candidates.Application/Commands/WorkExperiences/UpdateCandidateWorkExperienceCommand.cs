using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Shared;
using Domain.Seedwork.Enums;

namespace Candidates.Application.Commands.WorkExperiences
{
    public record UpdateCandidateWorkExperienceCommand(
        Guid CandidateId,        
        Guid WorkExperienceId,
        WorkExperienceType Type,
        string CompanyName,
        Position Position,
        DateTimeOffset From,
        DateTimeOffset? To,
        string? JobDescription,
        bool IsCurrentJob,
        IEnumerable<Skill> Skills,
        Guid UserId,
        string Scope
    ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
