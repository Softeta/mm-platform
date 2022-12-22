using EventBus.Subscribers.Interfaces;

namespace Jobs.API.Services
{
    public class EventBusHostedService : IHostedService
    {
        private readonly ICompaniesEventBusSubscriber _companiesEventBusSubscriber;
        private readonly ICandidatesEventBusSubscriber _candidatesEventBusSubscriber;
        private readonly IBackOfficeUsersEventBusSubscriber _backOfficeUsersEventBusSubscriber;
        private readonly ICandidateJobsEventBusSubscriber _candidateJobsEventBusSubscriber;
        private readonly IContactPersonsEventBusSubscriber _contactPersonEventBusSubscriber;
        private readonly ISkillsEventBusSubscriber _skillsEventBusSubscriber;
        private readonly IJobPositionsEventBusSubscriber _jobPositionsEventBusSubscriber;
        private readonly ILogger<EventBusHostedService> _logger;

        public EventBusHostedService(
            ICompaniesEventBusSubscriber companiesEventBusSubscriber, 
            ICandidatesEventBusSubscriber candidatesEventBusSubscriber,
            IBackOfficeUsersEventBusSubscriber backOfficeUsersEventBusSubscriber,
            ICandidateJobsEventBusSubscriber candidateJobsEventBusSubscriber,
            IContactPersonsEventBusSubscriber contactPersonEventBusSubscriber,
            ISkillsEventBusSubscriber skillsEventBusSubscriber,
            IJobPositionsEventBusSubscriber jobPositionsEventBusSubscriber,
            ILogger<EventBusHostedService> logger)
        {
            _companiesEventBusSubscriber = companiesEventBusSubscriber;
            _candidatesEventBusSubscriber = candidatesEventBusSubscriber;
            _backOfficeUsersEventBusSubscriber = backOfficeUsersEventBusSubscriber;
            _candidateJobsEventBusSubscriber = candidateJobsEventBusSubscriber;
            _contactPersonEventBusSubscriber = contactPersonEventBusSubscriber;
            _skillsEventBusSubscriber = skillsEventBusSubscriber;
            _jobPositionsEventBusSubscriber = jobPositionsEventBusSubscriber;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Event bus JobService subscribers");
            await Task.WhenAll(
                _companiesEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _candidatesEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _backOfficeUsersEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _candidateJobsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _contactPersonEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _skillsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.RegisterSubscriptionClientAsync()
            );
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Stopping Event bus JobService subscribers");
            await Task.WhenAll(
                _companiesEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _candidatesEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _backOfficeUsersEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _candidateJobsEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _contactPersonEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _skillsEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.UnRegisterSubscriptionClientAsync()
            );

            await Task.WhenAll(
                _companiesEventBusSubscriber.CloseAsync(),
                _candidatesEventBusSubscriber.CloseAsync(),
                _backOfficeUsersEventBusSubscriber.CloseAsync(),
                _candidateJobsEventBusSubscriber.CloseAsync(),
                _contactPersonEventBusSubscriber.CloseAsync(),
                _skillsEventBusSubscriber.CloseAsync(),
                _jobPositionsEventBusSubscriber.CloseAsync()
            );
            _logger.LogWarning($"Stopped Event bus JobService subscribers");
        }
    }
}
