using Jobs.Infrastructure.Persistence;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJobContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<JobContext>(options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()))
                .AddScoped<IJobContext, JobContext>()
                .AddScoped<IJobRepository, JobRepository>()
                .AddScoped<IJobCandidatesRepository, JobCandidatesRepository>()
                .AddScoped<IJobCandidatesFilterRepository, JobCandidatesFilterRepository>();
        }
    }
}
