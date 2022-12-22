using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;

namespace Jobs.Application.Commands.Filters;

public record AddCandidatesFilterCommand(
    Guid JobId,
    Guid UserId,
    int Index,
    string Title,
    string FilterParams
    ) : IRequest<JobCandidatesFilter>;