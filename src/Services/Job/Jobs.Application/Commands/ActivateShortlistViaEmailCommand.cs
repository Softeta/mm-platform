using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ActivateShortlistViaEmailCommand(Guid JobId, string Email) : IRequest<JobCandidates>;
}
