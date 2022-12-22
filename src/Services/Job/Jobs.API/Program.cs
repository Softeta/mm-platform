using API.Customization.Authentication;
using API.Customization.Authentication.Constants;
using API.Customization.Authorization;
using API.Customization.Authorization.Policies;
using API.Customization.Exceptions;
using API.Customization.Extensions;
using API.Customization.KeyVault;
using API.Customization.Middleware;
using API.Customization.Swagger;
using API.WebClients.Clients.HereSearch.Configurations;
using API.WebClients.Extensions;
using EventBus;
using EventBus.Constants;
using Jobs.API.Constants;
using Jobs.API.Services;
using Jobs.Application.Commands;
using Jobs.Application.EventBus;
using Jobs.Application.IntegrationEventHandlers;
using Jobs.Infrastructure.Extensions;
using MediatR;

var builder = WebApplication.CreateBuilder(args)
    .AddGlobalAzureKeyVault();

builder.Services
    .AddAzureAdAuthentication(builder.Configuration)
    .AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCompanies, builder.Configuration);

builder.Services
    .AddAzureAdB2CCandidatesAuthentication(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies, AuthenticationSchemas.AzureAdB2CCandidates);
    options.AddModifyJobPolicy(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies);
    options.AddBackOfficeUserModifyJobPolicy();
    options.AddSeeDashboardPolicy();
    options.AdministrationActionPolicy();
});

builder.Services.AddControllers()
    .AddControllerOptions();

builder.Services
    .AddApplicationInsightsTelemetry()
    .AddEndpointsApiExplorer()
    .AddSwagger(builder, "Jobs.API")
    .AddPolicyHandlers()
    .AddIntegrationEventHandlers()
    .AddEventBus(builder.Configuration[KeyVaultSecretNames.ServiceBusConnectionString])
    .AddCandidatesEventBusSubscriber(Topics.CandidateChanged.Subscribers.JobServiceFilters)
    .AddCompaniesEventBusSubscriber(Topics.CompanyChanged.Subscribers.JobServiceFilters)
    .AddBackOfficeUsersEventBusSubscriber(Topics.BackOfficeUserChanged.Subscribers.JobServiceFilters)
    .AddCandidateJobsEventBusSubscriber(Topics.CandidateJobsChanged.Subscribers.JobServiceFilters)
    .AddContactPersonsEventBusSubscriber(Topics.ContactPersonChanged.Subscribers.JobServiceFilters)
    .AddSkillsEventBusSubscriber(Topics.SkillChanged.Subscribers.JobServiceFilters)
    .AddJobPositionsEventBusSubscriber(Topics.JobPositionChanged.Subscribers.JobServiceFilters)
    .AddEventBusPublishers()
    .AddHostedService<EventBusHostedService>()
    .AddJobContext(builder.Configuration[KeyVaultSecretNames.DatabaseConnectionString])
    .AddHereSearchWebApiClient(builder.Configuration.GetHereSearchApiOptions().Uri)
    .AddMediatR(typeof(UpdateJobCommand).Assembly)
    .AddHealthChecks();

builder.ConfigureExceptionOptions();

var app = builder.Build();

app.UseErrorHandler();
app.Services.MigrateDatabase();

var swaggerOptions = builder.Configuration.GetSwaggerOptions();
var azureAdSettings = builder.Configuration.GetAzureAdSettings();

if (swaggerOptions.IsTurnedOn)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerJob v1");
        c.OAuthClientId(azureAdSettings.ApplicationId);
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseLoggingScope();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
});

app.Run();
