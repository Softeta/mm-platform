using Companies.Infrastructure.Clients;
using Companies.Infrastructure.Persistence;
using Companies.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Companies.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCompanyContext(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<CompanyContext>(options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()))
                .AddScoped<ICompanyContext, CompanyContext>()
                .AddScoped<ICompanyRepository, CompanyRepository>()
                .AddScoped<IRcCompanyRepository, RcCompanyRepository>();
        }

        public static IServiceCollection AddMsGraphServiceClient(this IServiceCollection services)
        {
            return services.AddSingleton<IMsGraphServiceClient, MsGraphServiceClient>();
        }
    }
}
