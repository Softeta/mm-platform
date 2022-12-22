using Domain.Seedwork;
using Domain.Seedwork.Exceptions;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Infrastructure.Persistence.Repositories
{
    public class JobCandidatesRepository : IJobCandidatesRepository
    {
        private readonly JobContext _context = null!;

        public IUnitOfWork UnitOfWork => _context;

        public JobCandidatesRepository(JobContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public JobCandidates Add(JobCandidates jobCandidates)
        {
            return _context.JobCandidates.Add(jobCandidates).Entity;
        }

        public async Task<JobCandidates> GetAsync(Guid id)
        {
            var job = await _context.JobCandidates
                .Include(j => j.SelectedCandidates)
                .Include(j => j.ArchivedCandidates)
                .AsSplitQuery()
                .Where(j => j.Id == id)
                .SingleOrDefaultAsync();

            if (job == null)
            {
                throw new NotFoundException("Job candidates was not found", ErrorCodes.NotFound.JobCandidatesNotFound);
            }

            return job;
        }

        public async Task<JobCandidates?> GetIfExistAsync(Guid id)
        {
            var job = await _context.JobCandidates
                .Include(j => j.SelectedCandidates)
                .Include(j => j.ArchivedCandidates)
                .AsSplitQuery()
                .Where(j => j.Id == id)
                .SingleOrDefaultAsync();

            return job;
        }

        public void Update(JobCandidates jobCandidates)
        {
            _context.Entry(jobCandidates).State = EntityState.Modified;
        }
    }
}
