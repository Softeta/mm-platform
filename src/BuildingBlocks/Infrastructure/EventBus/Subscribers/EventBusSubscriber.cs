using Azure.Messaging.ServiceBus;
using EventBus.EventHandlers;
using EventBus.Filters;
using EventBus.Subscribers.Interfaces;
using Microsoft.Extensions.Logging;

namespace EventBus.Subscribers
{
    public class EventBusSubscriber : 
        ICompaniesEventBusSubscriber,
        ICandidatesEventBusSubscriber, 
        IBackOfficeUsersEventBusSubscriber, 
        IJobsEventBusSubscriber,
        IJobCandidatesEventBusSubscriber,
        ICandidateJobsEventBusSubscriber,
        ISchedulerEventBusSubscriber,
        IContactPersonsEventBusSubscriber,
        ISkillsEventBusSubscriber,
        IJobPositionsEventBusSubscriber
    {
        private readonly IServiceBusFiltersManager _serviceBusFiltersManager;
        private readonly ISubscribersEventHandlersManager _subscribersManager;
        private readonly ServiceBusProcessor _processor;
        private readonly string _subscriptionName;
        private readonly string _topicName;
        private readonly string[] _filterNames;
        private readonly ILogger<EventBusSubscriber> _logger;

        public EventBusSubscriber(
            IServiceBusFiltersManager serviceBusFiltersManager,
            ISubscribersEventHandlersManager subscribersManager,
            ServiceBusClient serviceBusClient,
            string subscriptionName,
            string topicName,
            string[] filterNames,
            ILogger<EventBusSubscriber> logger)
        {
            var processorOptions = new ServiceBusProcessorOptions
            {
                AutoCompleteMessages = false
            };

            _processor = serviceBusClient.CreateProcessor(topicName, subscriptionName, processorOptions);
            _subscribersManager = subscribersManager;
            _subscriptionName = subscriptionName;
            _topicName = topicName;
            _filterNames = filterNames;
            _serviceBusFiltersManager = serviceBusFiltersManager;
            _logger = logger;
        }

        public async Task RegisterSubscriptionClientAsync()
        {
            await _serviceBusFiltersManager.CreateFiltersAsync(_topicName, _subscriptionName, _filterNames);

            _processor.ProcessMessageAsync += HandleMessageAsync;
            _processor.ProcessErrorAsync += HandleErrorAsync;

            await _processor.StartProcessingAsync();
        }

        public async Task UnRegisterSubscriptionClientAsync()
        {
            #pragma warning disable CA2254
            _logger.LogWarning(message: $"Stopping Topic: [{_topicName}] and Subscription: [{_subscriptionName}] processing");
            #pragma warning restore CA2254
            await _processor.StopProcessingAsync();
        }

        public async Task CloseAsync()
        {
            await _processor.CloseAsync();
        }

        private async Task HandleMessageAsync(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();
            var filterName = args.Message.Subject;

            if (string.IsNullOrWhiteSpace(filterName))
            {
                return;
            }

            var handler = _subscribersManager.GetHandler(filterName);

            if (handler is null)
            {
                return;
            }

            if (await handler.ExecuteAsync(message))
            {
                await args.CompleteMessageAsync(args.Message);
            }
        }

        private Task HandleErrorAsync(ProcessErrorEventArgs args)
        {
            #pragma warning disable CA2254
            _logger.LogError($"Failed receiving message for Topic: [{_topicName}] and Subscription: [{_subscriptionName}]", args.Exception);
            #pragma warning restore CA2254
            return Task.CompletedTask;
        }
    }
}
