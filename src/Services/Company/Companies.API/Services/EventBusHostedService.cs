using EventBus.Subscribers.Interfaces;

namespace Companies.API.Services
{
    public class EventBusHostedService : IHostedService
    {
        private readonly ISchedulerEventBusSubscriber _schedulerEventBusSubscriber;
        private readonly IContactPersonsEventBusSubscriber _contactPersonsEventBusSubscriber;
        private readonly ICompaniesEventBusSubscriber _companiesEventBusSubscriber;
        private readonly IJobsEventBusSubscriber _jobsEventBusSubscriber;
        private readonly IJobPositionsEventBusSubscriber _jobPositionsEventBusSubscriber;
        private readonly ILogger<EventBusHostedService> _logger;

        public EventBusHostedService(
            ISchedulerEventBusSubscriber schedulerEventBusSubscriber,
            IContactPersonsEventBusSubscriber contactPersonsEventBusSubscriber,
            ICompaniesEventBusSubscriber companiesEventBusSubscriber,
            IJobsEventBusSubscriber jobsEventBusSubscriber,
            IJobPositionsEventBusSubscriber jobPositionsEventBusSubscriber,
            ILogger<EventBusHostedService> logger)
        {
            _schedulerEventBusSubscriber = schedulerEventBusSubscriber;
            _contactPersonsEventBusSubscriber = contactPersonsEventBusSubscriber;
            _companiesEventBusSubscriber = companiesEventBusSubscriber;
            _jobsEventBusSubscriber = jobsEventBusSubscriber;
            _jobPositionsEventBusSubscriber = jobPositionsEventBusSubscriber;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Event bus CompaniesService subscribers");

            await Task.WhenAll(
                _schedulerEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _contactPersonsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _companiesEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _jobsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.RegisterSubscriptionClientAsync()
            );
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Stopping Event bus CompaniesService subscribers");

            await Task.WhenAll(
                _schedulerEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _contactPersonsEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _companiesEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _jobsEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.UnRegisterSubscriptionClientAsync()
            );

            await Task.WhenAll(
                _schedulerEventBusSubscriber.CloseAsync(),
                _contactPersonsEventBusSubscriber.CloseAsync(),
                _companiesEventBusSubscriber.CloseAsync(),
                _jobsEventBusSubscriber.CloseAsync(),
                _jobPositionsEventBusSubscriber.CloseAsync()
            );
            
            _logger.LogWarning($"Stopped Event bus CompaniesService subscribers");
        }
    }
}
