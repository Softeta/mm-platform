using API.Customization.Authentication.Constants;
using API.Customization.Authorization.RequirementHandlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;

namespace API.Customization.Authorization
{
    public static class StartupExtensions
    {
        public static AuthenticationBuilder AddAzureAdAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services.AddGenericAzureAdAuthentication(AuthenticationSchemas.AzureAd, configuration);
        }

        public static IServiceCollection AddAzureAdB2CCandidatesAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(configuration, AuthenticationSchemas.AzureAdB2CCandidates, AuthenticationSchemas.AzureAdB2CCandidates);

            return services;
        }

        public static IServiceCollection AddAzureAdB2CCompaniesAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddMicrosoftIdentityWebApiAuthentication(configuration, AuthenticationSchemas.AzureAdB2CCompanies, AuthenticationSchemas.AzureAdB2CCompanies);

            return services;
        }

        private static AuthenticationBuilder AddGenericAzureAdAuthentication(
            this IServiceCollection services,
            string authenticationSchema,
            ConfigurationManager configuration)
        {
            var authenticationBuilder = services.AddAuthentication(authenticationSchema)
                .AddJwtBearer(authenticationSchema, options =>
                {
                    configuration.Bind(authenticationSchema, options);
                });

            return authenticationBuilder;
        }

        public static AuthenticationBuilder AddAzureAdB2CAuthentication(this AuthenticationBuilder authenticationBuilder, string authenticationSchema, ConfigurationManager configuration)
        {
            authenticationBuilder
                .AddMicrosoftIdentityWebApi(
                    configuration,
                    authenticationSchema,
                    authenticationSchema);

            return authenticationBuilder;
        }

        public static void AddDefaultPolicy(this AuthorizationOptions authorizationOptions, params string[] authenticationSchemas)
        {
            authorizationOptions.DefaultPolicy = new AuthorizationPolicyBuilder(authenticationSchemas)
                .RequireAuthenticatedUser()
                .Build();
        }

        public static IServiceCollection AddPolicyHandlers(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, HasBackOfficeUserRolesHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanModifyJobHandler>();
            services.AddSingleton<IAuthorizationHandler, CompanyCanModifyJobHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanModifyCompanyHandler>();
            services.AddSingleton<IAuthorizationHandler, CompanyCanModifyHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, ContactPersonCanRegisterHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanModifyCandidateHandler>();
            services.AddSingleton<IAuthorizationHandler, CandidateCanModifyHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, CandidateCanRegisterHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, ContactPersonCanReadPublicCompanyHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanReadPublicCompanyHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanReadCompanyHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanReadCandidateHandler>();
            services.AddSingleton<IAuthorizationHandler, CandidateCanReadHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, HasBackOfficeUserAnyRoleHandler>();
            services.AddSingleton<IAuthorizationHandler, CandidateCanReadSingleHimselfHandler>();
            services.AddSingleton<IAuthorizationHandler, BackOfficeUserCanReadSingleCandidateHandler>();
            services.AddSingleton<IAuthorizationHandler, CompanyCanReadSingleCandidateHandler>();
            services.AddSingleton<IAuthorizationHandler, CompanyCanReadHimselfHandler>();

            return services;
        }
    }
}
