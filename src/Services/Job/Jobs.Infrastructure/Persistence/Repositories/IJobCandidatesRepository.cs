using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Infrastructure.Persistence.Repositories
{
    public interface IJobCandidatesRepository : IRepository<JobCandidates>
    {
        Task<JobCandidates> GetAsync(Guid id);

        Task<JobCandidates?> GetIfExistAsync(Guid id);

        JobCandidates Add(JobCandidates jobCandidates);

        void Update(JobCandidates jobCandidates);
    }
}
