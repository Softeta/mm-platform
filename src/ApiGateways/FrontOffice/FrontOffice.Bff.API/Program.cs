using API.Customization.Authorization;
using API.Customization.Authorization.Policies;
using API.Customization.Exceptions;
using API.Customization.Cors;
using API.Customization.Extensions;
using API.Customization.KeyVault;
using API.Customization.Middleware;
using API.Customization.Swagger;
using API.Customization.Yarp;
using API.Customization.Authentication.Constants;
using FrontOffice.Bff.API.Infrastructure.Extensions;
using FrontOffice.Bff.API.Infrastructure;
using API.Customization.Authentication;

var builder = WebApplication.CreateBuilder(args)
    .AddYarpConfiguration()
    .AddGlobalAzureKeyVault();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.GetYarpConfiguration());

builder.Services
    .AddOptions<ElasticSearchConfigurations>()
    .BindConfiguration("ElasticSearch");

builder.Services
    .AddOptions<CountrySettings>()
    .BindConfiguration("CountrySettings");

builder.Services
    .AddAzureAdB2CCandidatesAuthentication(builder.Configuration)
    .AddAzureAdB2CCompaniesAuthentication(builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy();
    options.AddSelfModificationCandidatePolicy();
    options.AddSelfRegistrationContactPersonPolicy();
    options.AdministrationActionPolicy();
    options.AddModifyCandidatePolicy(AuthenticationSchemas.AzureAdB2CCandidates);
    options.AddReadCandidatePolicy(AuthenticationSchemas.AzureAdB2CCandidates);
    options.AddReadCompanyPolicy(AuthenticationSchemas.AzureAdB2CCompanies);
    options.AddReadSingleCandidatePolicy(
        AuthenticationSchemas.AzureAdB2CCandidates,
        AuthenticationSchemas.AzureAdB2CCompanies
    );
    options.AddModifyJobPolicy(AuthenticationSchemas.AzureAdB2CCompanies);
});

builder.Services
    .AddApplicationInsightsTelemetry()
    .AddControllers()
    .AddControllerOptions();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddResponseCaching()
    .AddHealthChecks();

builder.Services.AddSwaggerForAdB2CCandidates(builder, "FrontOffice.API");
builder.Services.AddPolicyHandlers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddServiceClients(
    builder
    .Configuration
    .GetSection("Services")
    .Get<Services>());

builder.ConfigureExceptionOptions();

builder.Services.AddPolicyHandlers();
builder.Services.AddCors(
    builder
        .Configuration
        .GetSection("Cors")
        .Get<CorsOptions>());

builder.ConfigureExceptionOptions();

var app = builder.Build();
app.UseErrorHandler();

var swaggerOptions = builder.Configuration.GetSwaggerOptions();

if (swaggerOptions.IsTurnedOn)
{
    var azureAdB2CSettings = builder.Configuration.GetAzureAdB2CCandidatesSettings();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerFrontOffice v1");
        c.OAuthClientId(swaggerOptions.ApplicationId);
        c.OAuthScopes($"https://{azureAdB2CSettings.Domain}/{azureAdB2CSettings.Scope}");
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
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
