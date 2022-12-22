using AdministrationSettings.API.Infrastructure.Extensions;
using API.Customization.Authentication;
using API.Customization.Authentication.Constants;
using API.Customization.Authorization;
using API.Customization.Exceptions;
using API.Customization.Extensions;
using API.Customization.KeyVault;
using API.Customization.Middleware;
using API.Customization.Swagger;
using API.WebClients.Clients.HereSearch.Configurations;
using API.WebClients.Extensions;

var builder = WebApplication.CreateBuilder(args)
    .AddGlobalAzureKeyVault();

var auth = builder.Services.AddAzureAdAuthentication(builder.Configuration);
auth.AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCompanies, builder.Configuration);
auth.AddAzureAdB2CAuthentication(AuthenticationSchemas.AzureAdB2CCandidates, builder.Configuration);

builder.Services.AddAuthorization(options =>
{
    options.AddDefaultPolicy(AuthenticationSchemas.AzureAd, AuthenticationSchemas.AzureAdB2CCompanies, AuthenticationSchemas.AzureAdB2CCandidates);
});

builder.Services.AddControllers()
    .AddControllerOptions();

builder.Services
    .AddApplicationInsightsTelemetry()
    .AddEndpointsApiExplorer()
    .AddSwagger(builder, "AdministrationSettings.API")
    .AddPolicyHandlers()
    .AddHereSearchWebApiClient(builder.Configuration.GetHereSearchApiOptions().Uri)
    .AddHealthChecks();
    
builder
    .ConfigureExceptionOptions()
    .AddConfigurations();

var app = builder.Build();

app.UseErrorHandler();

var swaggerOptions = builder.Configuration.GetSwaggerOptions();
var azureAdSettings = builder.Configuration.GetAzureAdSettings();

if (swaggerOptions.IsTurnedOn)
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "swaggerAdministrationSettings v1");
        c.OAuthClientId(azureAdSettings.ApplicationId);
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseResponseCaching();
app.UseAuthentication();
app.UseAuthorization();
app.UseLoggingScope();
app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/api/health");
});

app.Run();

