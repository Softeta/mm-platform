using Domain.Seedwork;
using Jobs.Domain.Aggregates.JobAggregate;

namespace Jobs.Infrastructure.Persistence.Repositories
{
    public interface IJobRepository : IRepository<Job>
    {
        Task<Job> GetAsync(Guid id);
        Job Add(Job job);
        void Update(Job job);
        Task RemoveAsync(Guid id);
    }
}
