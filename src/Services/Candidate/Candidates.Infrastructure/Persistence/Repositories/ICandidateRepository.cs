using Candidates.Domain.Aggregates.CandidateAggregate;
using Domain.Seedwork;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Candidate Add(Candidate candidate);
        Task<Candidate> GetAsync(Guid id);
        Task<Candidate?> GetByExternalidAsync(Guid externalId);
        Task<Candidate?> GetByEmailAsync(string email);
        void Update(Candidate candidate);
        Task RemoveAsync(Guid id);
    }
}
