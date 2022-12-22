using Candidates.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Candidates.Infrastructure.Extensions;

public static class MigrateDatabaseExtension
{
    public static void MigrateDatabase(this IServiceProvider serviceProvider)
    {
        var candidateDb = serviceProvider
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<CandidateContext>();

        candidateDb.Database.Migrate();
    }
}
