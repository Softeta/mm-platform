using API.Customization.Authentication;
using API.Customization.Authentication.Constants;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Customization.Swagger
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            WebApplicationBuilder builder,
            string apiName)
        {
            var azureAdSettings = builder.Configuration.GetAzureAdSettings();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });
                opt.OperationFilter<PropertyIgnoreFilter>();
                opt.AddSecurityDefinition(AuthenticationSchemas.AzureAd, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    In = ParameterLocation.Header,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri($"{azureAdSettings.Authority}{azureAdSettings.AuthorizationUri}"),
                            TokenUrl = new Uri($"{azureAdSettings.Authority}{azureAdSettings.TokenUri}"),
                            Scopes = new Dictionary<string, string>
                            {
                                {$"{azureAdSettings.Audience}/{azureAdSettings.ApiScope}", azureAdSettings.ApiScope}
                            }
                        }
                    }
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = AuthenticationSchemas.AzureAd
                           }
                       },
                       new[] {azureAdSettings.ApiScope }
                    }
                });
            });

            return services;
        }


        public static IServiceCollection AddSwaggerForAdB2CCandidates(
            this IServiceCollection services,
             WebApplicationBuilder builder,
             string apiName)
        {
            var azureAdB2CSettings = builder.Configuration.GetAzureAdB2CCandidatesSettings();

            services.AddSwaggerGen(document =>
            {
                document.SwaggerDoc("v1", new OpenApiInfo { Title = apiName, Version = "v1" });
                document.OperationFilter<PropertyIgnoreFilter>();
                document.AddSecurityDefinition(AuthenticationSchemas.AzureAdB2CCandidates, new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Description = "B2C Candidates authentication",
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            Scopes = new Dictionary<string, string>
                        {
                            { $"https://{azureAdB2CSettings.Domain}/{azureAdB2CSettings.Scope}", azureAdB2CSettings.Scope}
                        },
                            AuthorizationUrl = new Uri($"{azureAdB2CSettings.Instance}/{azureAdB2CSettings.Domain}/oauth2/v2.0/authorize?p={azureAdB2CSettings.SignUpSignInPolicyId}"),
                            TokenUrl = new Uri($"{azureAdB2CSettings.Instance}/{azureAdB2CSettings.Domain}/oauth2/v2.0/token?p={azureAdB2CSettings.SignUpSignInPolicyId}")
                        },
                    }
                });
                document.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                   {
                       new OpenApiSecurityScheme
                       {
                           Reference = new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = AuthenticationSchemas.AzureAdB2CCandidates
                           }
                       },
                       new[] { $"https://{azureAdB2CSettings.Domain}/{azureAdB2CSettings.Scope}" }
                    }
                });
            });

            return services;
        }
    }
}
