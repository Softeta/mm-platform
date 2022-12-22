using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Commands.Filters;

public class AddCandidatesFilterCommandHandler : IRequestHandler<AddCandidatesFilterCommand, JobCandidatesFilter>
{
    private const int MaxFiltersCount = 3;

    private readonly IJobCandidatesFilterRepository _candidatesFilterRepository;

    public AddCandidatesFilterCommandHandler(IJobCandidatesFilterRepository candidatesFilterRepository)
    {
        _candidatesFilterRepository = candidatesFilterRepository;
    }

    public async Task<JobCandidatesFilter> Handle(AddCandidatesFilterCommand request, CancellationToken cancellationToken)
    {
        var existingFilters = await _candidatesFilterRepository.GetAllAsync(request.UserId, request.JobId);
        if (existingFilters.Count() >= MaxFiltersCount)
        {
            throw new BadRequestException($"Maximum filters count is {MaxFiltersCount}", ErrorCodes.Filters.Candidates.MaxCandidatesFilterCount);
        }

        return await _candidatesFilterRepository.AddAsync(new JobCandidatesFilter
        {
            UserId = request.UserId,
            FilterParams = request.FilterParams,
            Index = request.Index,
            JobId = request.JobId,
            Title = request.Title
        });
    }
}