using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Shared.Requests;

namespace Candidates.Application.Commands.Courses
{
    public record AddCandidateCourseCommand(
        Guid CandidateId,
        string Name,
        string IssuingOrganization,
        string? Description,
        AddFileCacheRequest? Certificate,
        Guid UserId,
        string Scope
        ) : ModifyCandidateBaseCommand<Candidate>(CandidateId, UserId, Scope);
}
