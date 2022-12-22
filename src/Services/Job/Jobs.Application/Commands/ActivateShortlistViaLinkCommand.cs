using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using MediatR;

namespace Jobs.Application.Commands
{
    public record ActivateShortlistViaLinkCommand(Guid JobId) : IRequest<JobCandidates>;
}
