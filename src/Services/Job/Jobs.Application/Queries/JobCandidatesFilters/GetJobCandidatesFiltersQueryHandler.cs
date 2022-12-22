using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;

namespace Jobs.Application.Queries.JobCandidatesFilters;

public class GetJobCandidatesFiltersQueryHandler : IRequestHandler<GetJobCandidatesFiltersQuery, IEnumerable<JobCandidatesFilter>>
{
    private readonly IJobCandidatesFilterRepository _candidatesFilterRepository;

    public GetJobCandidatesFiltersQueryHandler(IJobCandidatesFilterRepository candidatesFilterRepository)
    {
        _candidatesFilterRepository = candidatesFilterRepository;
    }

    public async Task<IEnumerable<JobCandidatesFilter>> Handle(GetJobCandidatesFiltersQuery request, CancellationToken cancellationToken)
    {
        return await _candidatesFilterRepository.GetAllAsync(request.UserId, request.JobId);
    }
}