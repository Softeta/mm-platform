using Candidates.Infrastructure.Clients.MicrosoftGraph;
using Candidates.Infrastructure.Clients.Talogy;
using Candidates.Infrastructure.Clients.Talogy.Authorization;
using Candidates.Infrastructure.Clients.Talogy.Configurations;
using Candidates.Infrastructure.Clients.Talogy.Constants;
using Candidates.Infrastructure.Persistence;
using Candidates.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace Candidates.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCandidateContext(this IServiceCollection services, string connectionString)
    {
        return services
            .AddDbContext<CandidateContext>(
                options => options.UseSqlServer(connectionString, x => x.UseNetTopologySuite()))
            .AddScoped<ICandidateContext, CandidateContext>()
            .AddScoped<ICandidateRepository, CandidateRepository>()
            .AddScoped<ICandidateJobsRepository, CandidateJobsRepository>()
            .AddScoped<ICandidateTestsRepository, CandidateTestsRepository>();
    }

    public static IServiceCollection AddMsGraphServiceClient(this IServiceCollection services)
    {
        return services.AddSingleton<IMsGraphServiceClient, MsGraphServiceClient>();
    }

    public static IServiceCollection AddTalogyApiClient(
            this IServiceCollection services,
            ConfigurationManager configuration)
    {
        services.AddTalogyAuthWebApiClient(configuration);
        services.AddTalogyWebApiClient(configuration);

        services.AddSingleton<IAuthorizationManager, AuthorizationManager>();
        services.AddSingleton<ITalogyAuthApiClient, TalogyAuthApiClient>();
        services.AddSingleton<ITalogyApiClient, TalogyApiClient>();

        return services;
    }

    private static IServiceCollection AddTalogyWebApiClient(
            this IServiceCollection services,
            ConfigurationManager configuration)
    {
        var options = configuration.GetTalogyApiOptions();

        services.AddHttpClient(ApiClients.TalogApiClientName, client =>
        {
            client.BaseAddress = new Uri(options.Url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }

    private static IServiceCollection AddTalogyAuthWebApiClient(
            this IServiceCollection services,
            ConfigurationManager configuration)
    {
        var options = configuration.GetTalogyApiOptions();

        services.AddHttpClient(ApiClients.TalogyAuthApiClientName, client =>
        {
            client.BaseAddress = new Uri(options.AuthUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        return services;
    }
}
