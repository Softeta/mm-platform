using API.Customization.KeyVault;
using Azure.Messaging.ServiceBus.Administration;
using EmailService.Send.Commands.JobReceiver;
using EmailService.Send.Constants;
using EmailService.Send.Models.AppSettings;
using EventBus.Constants;
using EventBus.Filters;
using MediatR;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Customization.Storages;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Helpers;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(EmailService.Send.Startup))]
namespace EmailService.Send
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            builder.AddGlobalAzureKeyVault();
            base.ConfigureAppConfiguration(builder);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configurations = builder.GetContext().Configuration;
            var createFilterRulesTask = CreateFilterRulesAsync(configurations[KeyVaultSecretNames.ServiceBusConnectionString]);

            var storageAccountConfigurations = new StorageAccountConfigurations
            {
                AccountName = configurations[AppSettings.StorageAccountName],
                AccountKey = configurations[GlobalKeyVaultSecretNames.PrivateStorageAccountKey]
            };

            builder.Services.AddOptions<CandidateVerificationOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("CandidateVerification").Bind(settings);
                });

            builder.Services.AddOptions<ContactPersonVerificationOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("ContactPersonVerification").Bind(settings);
                });

            builder.Services.AddOptions<CandidateWelcomeOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("CandidateWelcome").Bind(settings);
                });
            builder.Services.AddOptions<CandidateRejectedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("CandidateRejected").Bind(settings);
                });
            builder.Services.AddOptions<ContactPersonWelcomeOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("ContactPersonWelcome").Bind(settings);
                });
            builder.Services.AddOptions<CompanyRejectedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("CompanyRejected").Bind(settings);
                });
            builder.Services.AddOptions<JobCandidateInvitedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("JobCandidateInvited").Bind(settings);
                });
            builder.Services.AddOptions<JobApprovedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("JobApproved").Bind(settings);
                });
            builder.Services.AddOptions<JobRejectedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("JobRejected").Bind(settings);
                });
            builder.Services.AddOptions<JobCandidatesShortlistActivatedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("JobCandidatesShortlistActivated").Bind(settings);
                });
            builder.Services.AddOptions<CandidateAppliedToJobOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("CandidateAppliedToJob").Bind(settings);
                });
            builder.Services.AddOptions<JobSubmittedOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("JobSubmitted").Bind(settings);
                });
            builder.Services.AddOptions<ContactPersonInvitedBackOfficeOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("ContactPersonInvitedByBO").Bind(settings);
                });
            builder.Services.AddOptions<ContactPersonInvitedClientOptions>()
                .Configure<IConfiguration>((settings, configurations) =>
                {
                    configurations.GetSection("ContactPersonInvitedByClient").Bind(settings);
                });
            builder.Services
                .AddPrivateTableServiceClient(storageAccountConfigurations.ConnectionString)
                .AddMediatR(typeof(SendAskedForJobApprovalCommand))
                .AddSendInBlueClient();

            var createEmailMessagesTableTask = StorageTableHelper.CreateIfNotExistAsync(
                                                    storageAccountConfigurations.ConnectionString,
                                                    EmailMessageTableStorageConstants.TableName);

            Task.WaitAll(
                createFilterRulesTask,
                createEmailMessagesTableTask
            );
        }

        private static async Task CreateFilterRulesAsync(string serviceBusConnectionString)
        {
            var adminClient = new ServiceBusAdministrationClient(serviceBusConnectionString);
            var serviceBusFilterManager = new ServiceBusFiltersManager(adminClient);

            await Task.WhenAll(
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.JobShareChanged.Name,
                        Topics.JobShareChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.JobShareChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.CandidateChanged.Name,
                        Topics.CandidateChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.CandidateChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.ContactPersonChanged.Name,
                        Topics.ContactPersonChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.ContactPersonChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.CompanyChanged.Name,
                        Topics.CompanyChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.CompanyChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.JobCandidatesChanged.Name,
                        Topics.JobCandidatesChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.JobCandidatesChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.JobChanged.Name,
                        Topics.JobChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.JobChanged.Subscribers.EmailServiceFilters.filterNames),
                serviceBusFilterManager.CreateFiltersAsync(
                        Topics.CandidateJobsChanged.Name,
                        Topics.CandidateJobsChanged.Subscribers.EmailServiceFilters.subscriptionName,
                        Topics.CandidateJobsChanged.Subscribers.EmailServiceFilters.filterNames)
            );
        }
    }
}
