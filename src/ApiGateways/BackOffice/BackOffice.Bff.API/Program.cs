using API.Customization.Authentication;
using API.Customization.Authentication.Constants;
using API.Customization.Authorization;
using API.Customization.Authorization.Policies;
using API.Customization.Cors;
using API.Customization.Exceptions;
using API.Customization.Extensions;
using API.Customization.KeyVault;
using API.Customization.Middleware;
using API.Customization.Swagger;
using API.Customization.Yarp;
using API.WebClients.Clients.FormRecognizer.Configurations;
using API.WebClients.Clients.HereSearch.Configurations;
using API.WebClients.Extensions;
using BackOffice.Application.Queries;
using BackOffice.Bff.API.Infrastructure;
using BackOffice.Bff.API.Infrastructure.Constants;
using BackOffice.Bff.API.Infrastructure.Extensions;
using BackOffice.Shared.Constants;
using BackOffice.Shared.Queries;
using MediatR;
using Persistence.Customization.FileStorage;
using Persistence.Customization.Queries;
using Persistence.Customization.Storages;

var builder = WebApplication.CreateBuilder(args)
    .AddYarpConfiguration()
    .AddGlobalAzureKeyVault();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.GetYarpConfiguration());

builder.Services
    .AddOptions<ElasticSearchConfigurations>()
    .BindConfiguration("ElasticSearch");

builder.Services.AddOptions<CvFormRecognizerSettings>().BindConfiguration("FormRecognizerSettings")
    .Configure(x => x.Key = builder.Configuration[KeyVaultSecretNames.CandidatesCvParserSecret]);

builder.Services.AddOptions<BlobContainerSettings>().BindConfiguration("BlobStorageContainers");   

builder.Services.AddOptions<WeavyJwtSettings>().BindConfiguration("WeavyJwt")
    .Configure(x => x.Key = builder.Configuration[KeyVaultSecrets.WeawyApiSecret]); ;

builder.Services.AddAzureAdAuthentication(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy();
    options.AddModifyJobPolicy(AuthenticationSchemas.AzureAd);
    options.AddBackOfficeUserModifyJobPolicy();
    options.AddSeeDashboardPolicy();
    options.AddModifyCompanyPolicy(AuthenticationSchemas.AzureAd);
    options.AddBackOfficeUserModifyCompanyPolicy();
    options.AddModifyCandidatePolicy(AuthenticationSchemas.AzureAd);
    options.AddBackOfficeUserModifyCandidatePolicy();
    options.AddBackOfficeUserReadCandidatePolicy();
    options.AddReadCompanyPolicy(AuthenticationSchemas.AzureAd);
    options.AddReadCandidatePolicy(AuthenticationSchemas.AzureAd);
    options.AddReadSingleCandidatePolicy(AuthenticationSchemas.AzureAd);
});

builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddControllers()
    .AddControllerOptions();

builder.Services.AddResponseCaching();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddSwagger(builder, "BackOffice.Bff.API");

builder.Services.AddPolicyHandlers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServiceClients(
    builder
    .Configuration
    .GetSection("Services")
    .Get<Services>());

builder.Services.AddWebTableServiceClient(
    builder
    .Configuration
    .GetStorageAccountConfigurations<StorageAccountConfigurations>("StorageAccount", KeyVaultSecretNames.MmWebStorageAccountKey)
    .ConnectionString);

builder.Services.AddPrivateTableServiceClient(
    builder
    .Configuration
    .GetStorageAccountConfigurations<StorageAccountConfigurations>("PrivateStorageAccount", GlobalKeyVaultSecretNames.PrivateStorageAccountKey)
    .ConnectionString);

builder.Services.AddCors(
    builder
        .Configuration
        .GetSection("Cors")
        .Get<CorsOptions>());

builder.Services.AddMediatR(typeof(GetCandidateFromCvQuery).Assembly);
builder.Services.AddMediatR(typeof(GetCachedBackOfficeUsersQuery).Assembly);

builder.ConfigureExceptionOptions();

builder.Services.BindPrivateStorageAccount(builder.Configuration, "PrivateStorageAccount")
    .AddPrivateFileClients()
    .AddFormRecognizer()
    .AddSharedQueries()
    .AddHereSearchWebApiClient(builder.Configuration.GetHereSearchApiOptions().Uri);

var app = builder.Build();

app.UseErrorHandler();

var swaggerOptions = builder.Configuration.GetSwaggerOptions();
var azureAdSettings = builder.Configuration.GetAzureAdSettings();

if (swaggerOptions.IsTurnedOn)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerBackOffice v1");
        c.OAuthClientId(azureAdSettings.ApplicationId);
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.UseLoggingScope();
app.MapControllers();
app.MapReverseProxy();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
});

app.Run();

