using Companies.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Infrastructure.Extensions
{
    public static class MigrateDatabaseExtensions
    {
        public static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            var companyDb = serviceProvider
                .CreateScope()
                .ServiceProvider
                .GetRequiredService<CompanyContext>();

            companyDb.Database.Migrate();
        }
    }
}
