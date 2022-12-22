using Jobs.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jobs.Infrastructure.Extensions
{
    public static class MigrateDatabaseExtension
    {
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var jobDb = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<JobContext>();

            jobDb.Database.Migrate();
        }
    }
}
