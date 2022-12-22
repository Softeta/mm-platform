using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Infrastructure.Persistence.Repositories;

public interface IJobCandidatesFilterRepository
{
    Task<JobCandidatesFilter> AddAsync(JobCandidatesFilter jobCandidatesFilter);
    Task<IEnumerable<JobCandidatesFilter>> GetAllAsync(Guid userId, Guid jobId);
    Task DeleteAsync(Guid jobId, Guid userId, int index);
    Task<JobCandidatesFilter?> UpdateTitleAsync(Guid jobId, Guid userId, int index, string title);
}