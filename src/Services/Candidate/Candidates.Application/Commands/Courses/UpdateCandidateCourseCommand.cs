using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Shared.Requests;

namespace Candidates.Application.Commands.Courses
{
    public record UpdateCandidateCourseCommand(
        Guid CandidateId,
        Guid CourseId,
        string Name,
        string IssuingOrganization,
        string? Description,
        UpdateFileCacheRequest Certificate,
        Guid UserId,
        string Scope
        ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
