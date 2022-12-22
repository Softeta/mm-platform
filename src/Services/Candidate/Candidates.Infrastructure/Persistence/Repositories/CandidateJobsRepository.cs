using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Domain.Seedwork;
using Microsoft.EntityFrameworkCore;

namespace Candidates.Infrastructure.Persistence.Repositories
{
    public class CandidateJobsRepository : ICandidateJobsRepository
    {
        private readonly CandidateContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CandidateJobsRepository(CandidateContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public CandidateJobs Add(CandidateJobs candidateJobs)
        {
            return _context.CandidateJobs.Add(candidateJobs).Entity;
        }

        public async Task<CandidateJobs?> GetAsync(Guid id)
        {
            var candidateJobs = await _context.CandidateJobs
                .Include(c => c.SelectedInJobs)
                .Include(c => c.ArchivedInJobs)
                .Include(c => c.AppliedInJobs)
                .AsSplitQuery()
                .Where(j => j.Id == id)
                .SingleOrDefaultAsync();

            return candidateJobs;
        }

        public void Update(CandidateJobs candidateJobs)
        {
            _context.Entry(candidateJobs).State = EntityState.Modified;
        }
    }
}
