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
using Candidates.API.Constants;
using Candidates.API.Services;
using Candidates.Application.Commands;
using Candidates.Application.EventBus;
using Candidates.Application.IntegrationEventHandlers;
using Candidates.Application.NotificationHandlers;
using Candidates.Infrastructure.Clients.Talogy.Configurations;
using Candidates.Infrastructure.Extensions;
using Candidates.Infrastructure.Settings;
using Custom.Attributes.Settings;
using EventBus;
using EventBus.Constants;
using MediatR;
using Persistence.Customization.Commands;
using Persistence.Customization.FileStorage;
using Persistence.Customization.ImageProcessing;
using Persistence.Customization.Queries;
using Persistence.Customization.Storages;

var builder = WebApplication.CreateBuilder(args)
    .AddGlobalAzureKeyVault();

builder.Services
    .AddAzureAdAuthentication(builder.Configuration)
    .AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCandidates, builder.Configuration)
    .AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCompanies, builder.Configuration);

builder.Services
    .BindPublicStorageAccount(builder.Configuration, "PublicStorageAccount")
    .BindPrivateStorageAccount(builder.Configuration, "PrivateStorageAccount");

builder.Services.AddOptions<TalogySettings>().BindConfiguration("Talogy");
builder.Services.AddOptions<SelfServiceSettings>().BindConfiguration("SelfService");
builder.Services.AddOptions<BlobContainerSettings>().BindConfiguration("BlobStorageContainers");
builder.Services.AddOptions<ImageSettings>().BindConfiguration("ImageSettings");
builder.Services.AddOptions<DocumentSettings>().BindConfiguration("DocumentSettings");
builder.Services.AddOptions<ImageProcessingSettings>().BindConfiguration("ImageProcessingSettings");
builder.Services.AddOptions<VerificationSettings>().BindConfiguration("VerificationSettings");
builder.Services.AddOptions<RegisteredCandidateSettings>().BindConfiguration("RegisteredCandidateSettings");
builder.Services.AddOptions<VideoSettings>().BindConfiguration("VideoSettings");
builder.Services.AddOptions<CacheSettings>().BindConfiguration("CacheSettings");
builder.Services.AddOptions<AppRegistrationSettings>().BindConfiguration(AuthenticationSchemas.AzureAdB2CCandidates)
    .Configure(x => x.ClientSecret = builder.Configuration[KeyVaultSecretNames.CandidateAppRegistrationSecret]);
builder.Services.AddOptions<TalogyConfigurations>().BindConfiguration("TalogyApi")
    .Configure(x => x.ClientSecret = builder.Configuration[KeyVaultSecretNames.TalogyApiClientSecrect]);

var storageAccountConfigurations = builder.Configuration.GetStorageAccountConfigurations<PrivateStorageAccountConfigurations>(
    "PrivateStorageAccount",
    Persistence.Customization.Storages.ServiceRegistrationExtensions.PrivateStorageAccountKey);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy();
    options.AddModifyCandidatePolicy(
        AuthenticationSchemas.AzureAd,
        AuthenticationSchemas.AzureAdB2CCandidates
    );
    options.AddReadCandidatePolicy(
        AuthenticationSchemas.AzureAd,
        AuthenticationSchemas.AzureAdB2CCandidates
    );
    options.AddReadSingleCandidatePolicy(
        AuthenticationSchemas.AzureAd,
        AuthenticationSchemas.AzureAdB2CCandidates,
        AuthenticationSchemas.AzureAdB2CCompanies
    );
    options.AddSelfModificationCandidatePolicy();
    options.AdministrationActionPolicy();
    options.AddBackOfficeUserModifyCandidatePolicy();
    options.AddBackOfficeUserReadCandidatePolicy();
});

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddControllers()
    .AddControllerOptions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwagger(builder, "Candidates.API");
builder.ConfigureExceptionOptions();
builder.Services.AddPolicyHandlers();
builder.Services
    .AddIntegrationEventHandlers()
    .AddJobCandidatesEventBusSubscriber(Topics.JobCandidatesChanged.Subscribers.CandidateServiceFilters)
    .AddSchedulerEventBusSubscriber(Topics.SchedulerJobScheduled.Subscribers.CandidateServiceFilters)
    .AddCandidatesEventBusSubscriber(Topics.CandidateChanged.Subscribers.CandidateServiceFilters)
    .AddSkillsEventBusSubscriber(Topics.SkillChanged.Subscribers.CandidateServiceFilters)
    .AddJobPositionsEventBusSubscriber(Topics.JobPositionChanged.Subscribers.CandidateServiceFilters)
    .AddHostedService<HostedService>()
    .AddEventBus(builder.Configuration[KeyVaultSecretNames.ServiceBusConnectionString])
    .AddEventBusPublishers()
    .AddCandidateContext(builder.Configuration[KeyVaultSecretNames.DatabaseConnectionString])
    .AddPublicFileClients()
    .AddPrivateFileClients()
    .AddImageProcessor()
    .AddHereSearchWebApiClient(builder.Configuration.GetHereSearchApiOptions().Uri)
    .AddMsGraphServiceClient()
    .AddPrivateTableServiceClient(storageAccountConfigurations.ConnectionString)
    .AddMediatR(typeof(InitializeCandidateCommand).Assembly)
    .AddGenericCommands()
    .AddSharedQueries()
    .AddTalogyApiClient(builder.Configuration)
    .AddNotificationHandlers();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerCandidate v1");
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
