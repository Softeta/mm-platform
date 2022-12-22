using Azure.Messaging.ServiceBus;
using EventBus.Constants;
using EventBus.EventHandlers;
using EventBus.Filters;
using EventBus.Subscribers;
using EventBus.Subscribers.Interfaces;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus
{
    public static class ServiceRegistrations
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string connectionString)
        {
            services.AddAzureClients(builder =>
            {
                builder.AddServiceBusClient(connectionString);
                builder.AddServiceAdministrationBusClient(connectionString);
            });

            services.AddSingleton<IServiceBusFiltersManager, ServiceBusFiltersManager>();

            return services;
        }

        public static IServiceCollection AddCompaniesEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<ICompaniesEventBusSubscriber, EventBusSubscriber>(sp => 
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.CompanyChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddContactPersonsEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<IContactPersonsEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.ContactPersonChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddCandidatesEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<ICandidatesEventBusSubscriber, EventBusSubscriber>(sp => 
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.CandidateChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddJobsEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<IJobsEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.JobChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddJobCandidatesEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<IJobCandidatesEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.JobCandidatesChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddBackOfficeUsersEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<IBackOfficeUsersEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.BackOfficeUserChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddCandidateJobsEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<ICandidateJobsEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.CandidateJobsChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddSchedulerEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<ISchedulerEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.SchedulerJobScheduled.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddSkillsEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<ISkillsEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.SkillChanged.Name, service.filterNames));

            return services;
        }

        public static IServiceCollection AddJobPositionsEventBusSubscriber(this IServiceCollection services, (string subscriptionName, string[] filterNames) service)
        {
            services.AddSingleton<IJobPositionsEventBusSubscriber, EventBusSubscriber>(sp =>
                BuildServiceBusSubscriber(sp, service.subscriptionName, Topics.JobPositionChanged.Name, service.filterNames));

            return services;
        }

        private static EventBusSubscriber BuildServiceBusSubscriber(IServiceProvider serviceProvider, string subscriptionName, string topicName, string[] filterNames)
        {
            var serviceBusFiltersManager = serviceProvider.GetRequiredService<IServiceBusFiltersManager>();
            var serviceBusClient = serviceProvider.GetRequiredService<ServiceBusClient>();
            var subscribersManager = serviceProvider.GetRequiredService<ISubscribersEventHandlersManager>();
            var logger = serviceProvider.GetRequiredService<ILogger<EventBusSubscriber>>();

            return new EventBusSubscriber(
                serviceBusFiltersManager,
                subscribersManager,
                serviceBusClient,
                subscriptionName,
                topicName,
                filterNames,
                logger);
        }
    }
}
