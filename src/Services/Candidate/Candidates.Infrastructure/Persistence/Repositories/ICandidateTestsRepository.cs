using Candidates.Domain.Aggregates.CandidateTestsAggregate;
using Domain.Seedwork;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public interface ICandidateTestsRepository : IRepository<CandidateTests>
    {
        CandidateTests Add(CandidateTests candidateTests);
        Task<CandidateTests?> GetAsync(Guid id);
        Task<CandidateTests?> GetAsync(long candidateOldPlatformid);
        void Update(CandidateTests candidateTests);
    }
}
