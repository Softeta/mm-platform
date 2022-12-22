using Azure.Core;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace API.Customization.KeyVault
{
    public static  class ServiceRegistrations
    {
        public static WebApplicationBuilder AddGlobalAzureKeyVault(this WebApplicationBuilder builder)
        {
            TokenCredential tokenCredential;

            if (builder.Environment.IsDevelopment())
            {
                if (bool.TryParse(builder.Configuration["IsDockerRunning"], out _))
                {
                    tokenCredential = new ChainedTokenCredential(new DefaultAzureCredential(), new EnvironmentCredential());
                }
                else
                {
                    var credentialOptions = new DefaultAzureCredentialOptions
                    {
                        VisualStudioTenantId = builder.Configuration[KeyVaultConstants.AuthorizationTenantId]
                    };

                    tokenCredential = new DefaultAzureCredential(credentialOptions);
                }
            }
            else
            {
                tokenCredential = new DefaultAzureCredential();
            }

            var globalKeyVaultName = builder.Configuration[KeyVaultConstants.GlobalKeyVault];
            builder.Configuration.AddGlobalAzureKeyVault(globalKeyVaultName, tokenCredential);

            return builder;
        }

        public static IFunctionsConfigurationBuilder AddGlobalAzureKeyVault(this IFunctionsConfigurationBuilder builder)
        {
            TokenCredential tokenCredential;

            if (bool.TryParse(Environment.GetEnvironmentVariable("IsDockerRunning"), out _))
            {
                tokenCredential = new ChainedTokenCredential(new DefaultAzureCredential(), new EnvironmentCredential());
            }
            else
            {
                var azureCredentialOptions = new DefaultAzureCredentialOptions
                {
                    VisualStudioTenantId = Environment.GetEnvironmentVariable(KeyVaultConstants.AuthorizationTenantId)
                };

                tokenCredential = new DefaultAzureCredential(azureCredentialOptions);
            }

            var globalKeyVaultName = Environment.GetEnvironmentVariable(KeyVaultConstants.GlobalKeyVault);
            builder.ConfigurationBuilder.AddGlobalAzureKeyVault(globalKeyVaultName, tokenCredential);

            return builder;
        }

        private static void AddGlobalAzureKeyVault(
            this IConfigurationBuilder configurationBuilder,
            string? globalKeyVaultName,
            TokenCredential tokenCredential)
        {
            if (string.IsNullOrWhiteSpace(globalKeyVaultName))
            {
                throw new ArgumentException($"Missing {KeyVaultConstants.GlobalKeyVault} name in app settings");
            }

            var globalKeyVaultUri = new Uri($"https://{globalKeyVaultName}.vault.azure.net/");
            var kvClient = new SecretClient(globalKeyVaultUri, tokenCredential);
            configurationBuilder.AddAzureKeyVault(kvClient, new KeyVaultSecretManager());
        }
    }
}
