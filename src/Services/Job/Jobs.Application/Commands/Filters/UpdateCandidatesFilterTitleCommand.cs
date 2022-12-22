using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using MediatR;

namespace Jobs.Application.Commands.Filters;

public record UpdateCandidatesFilterTitleCommand(
    Guid JobId, 
    Guid UserId, 
    int Index,
    string Title) : IRequest<JobCandidatesFilter>;