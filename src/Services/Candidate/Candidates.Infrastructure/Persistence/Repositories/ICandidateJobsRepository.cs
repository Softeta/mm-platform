using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Domain.Seedwork;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public interface ICandidateJobsRepository : IRepository<CandidateJobs>
    {
        CandidateJobs Add(CandidateJobs candidateJobs);
        Task<CandidateJobs?> GetAsync(Guid id);
        void Update(CandidateJobs candidateJobs);
    }
}
