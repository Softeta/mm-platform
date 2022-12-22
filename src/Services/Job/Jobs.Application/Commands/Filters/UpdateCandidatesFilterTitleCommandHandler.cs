using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands.Filters;

public class UpdateCandidatesFilterTitleCommandHandler : IRequestHandler<UpdateCandidatesFilterTitleCommand, JobCandidatesFilter>
{
    private readonly IJobCandidatesFilterRepository _candidatesFilterRepository;

    public UpdateCandidatesFilterTitleCommandHandler(IJobCandidatesFilterRepository candidatesFilterRepository)
    {
        _candidatesFilterRepository = candidatesFilterRepository;
    }

    public async Task<JobCandidatesFilter> Handle(UpdateCandidatesFilterTitleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _candidatesFilterRepository.UpdateTitleAsync(
            request.JobId, 
            request.UserId, 
            request.Index,
            request.Title);

        if (entity == null)
        {
            throw new NotFoundException("Filter not found", ErrorCodes.NotFound.CandidatesFilterNotFound);
        }

        return entity;
    }
}