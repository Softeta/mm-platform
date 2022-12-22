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
using Companies.API.Constants;
using Companies.API.Services;
using Companies.Application.Commands.ContactPersons;
using Companies.Application.EventBus;
using Companies.Application.IntegrationEventHandlers;
using Companies.Infrastructure.Extensions;
using Companies.Infrastructure.Settings;
using Custom.Attributes.Settings;
using EventBus;
using EventBus.Constants;
using MediatR;
using Persistence.Customization.Commands;
using Persistence.Customization.FileStorage;
using Persistence.Customization.ImageProcessing;
using Persistence.Customization.Queries;
using Persistence.Customization.Storages;
using BlobContainerSettings = Companies.Infrastructure.Settings.BlobContainerSettings;

var builder = WebApplication.CreateBuilder(args)
    .AddGlobalAzureKeyVault();

builder.Services.BindPublicStorageAccount(builder.Configuration, "PublicStorageAccount");
builder.Services.AddOptions<BlobContainerSettings>().BindConfiguration("BlobStorageContainers");
builder.Services.AddOptions<ImageSettings>().BindConfiguration("ImageSettings");
builder.Services.AddOptions<ImageProcessingSettings>().BindConfiguration("ImageProcessingSettings");
builder.Services.AddOptions<VerificationSettings>().BindConfiguration("VerificationSettings");
builder.Services.AddOptions<RegisteredContactPersonSettings>().BindConfiguration("RegisteredContactPersonSettings");
builder.Services.AddOptions<CacheSettings>().BindConfiguration("CacheSettings");
builder.Services.AddOptions<AppRegistrationSettings>().BindConfiguration(AuthenticationSchemas.AzureAdB2CCompanies)
    .Configure(x => x.ClientSecret = builder.Configuration[KeyVaultSecretNames.CompanyAppRegistrationSecret]);
builder.Services.AddOptions<CompanyB2CExtensionsAppSettings>().BindConfiguration("CompanyB2CExtensionsApp");

builder.Services
    .AddAzureAdAuthentication(builder.Configuration)
    .AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCompanies, builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy();
    options.AddModifyCompanyPolicy(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies);
    options.AddReadCompanyPolicy(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies);
    options.AddReadPublicCompanyPolicy();
    options.AddSelfRegistrationContactPersonPolicy();
    options.AddBackOfficeUserModifyCompanyPolicy();
    options.AdministrationActionPolicy();
});

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddControllers()
    .AddControllerOptions();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwagger(builder, "Companies.API");
builder.Services.AddPolicyHandlers();

var storageAccountConfigurations = builder.Configuration.GetStorageAccountConfigurations<PrivateStorageAccountConfigurations>(
    "PrivateStorageAccount",
    Persistence.Customization.Storages.ServiceRegistrationExtensions.PrivateStorageAccountKey);

builder.Services
    .AddIntegrationEventHandlers()
    .AddSchedulerEventBusSubscriber(Topics.SchedulerJobScheduled.Subscribers.CompanyServiceFilters)
    .AddContactPersonsEventBusSubscriber(Topics.ContactPersonChanged.Subscribers.CompanyServiceFilters)
    .AddCompaniesEventBusSubscriber(Topics.CompanyChanged.Subscribers.CompanyServiceFilters)
    .AddJobsEventBusSubscriber(Topics.JobChanged.Subscribers.CompanyServiceFilters)
    .AddJobPositionsEventBusSubscriber(Topics.JobPositionChanged.Subscribers.CompanyServiceFilters)
    .AddHostedService<EventBusHostedService>()
    .AddEventBus(builder.Configuration[KeyVaultSecretNames.ServiceBusConnectionString])
    .AddEventBusPublishers()
    .AddCompanyContext(builder.Configuration[KeyVaultSecretNames.DatabaseConnectionString])
    .AddPublicFileClients()
    .AddPrivateFileClients()
    .AddImageProcessor()
    .AddHereSearchWebApiClient(builder.Configuration.GetHereSearchApiOptions().Uri)
    .AddMsGraphServiceClient()
    .AddDanishRegistryCenterWebApiClient(builder.Configuration)
    .AddPrivateTableServiceClient(storageAccountConfigurations.ConnectionString)
    .AddMediatR(typeof(AddCompanyContactPersonCommand).Assembly)
    .AddGenericCommands()
    .AddSharedQueries();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerCompany v1");
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
