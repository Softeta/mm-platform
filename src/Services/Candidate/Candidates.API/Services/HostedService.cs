using EventBus.Subscribers.Interfaces;
using Microsoft.Extensions.Options;
using Persistence.Customization.Storages;
using Persistence.Customization.TableStorage;
using Persistence.Customization.TableStorage.Helpers;

namespace Candidates.API.Services
{
    public class HostedService : IHostedService
    {
        private readonly IJobCandidatesEventBusSubscriber _jobCandidatesEventBusSubscriber;
        private readonly ISchedulerEventBusSubscriber _schedulerEventBusSubscriber;
        private readonly ICandidatesEventBusSubscriber _candidatesEventBusSubscriber;
        private readonly ISkillsEventBusSubscriber _skillsEventBusSubscriber;
        private readonly IJobPositionsEventBusSubscriber _jobPositionsEventBusSubscriber;
        private readonly PrivateStorageAccountConfigurations _privateStorageConfigurations;
        private readonly ILogger<HostedService> _logger;

        public HostedService(
            IJobCandidatesEventBusSubscriber jobCandidatesEventBusSubscriber,
            ISchedulerEventBusSubscriber schedulerEventBusSubscriber,
            ICandidatesEventBusSubscriber candidatesEventBusSubscriber,
            ISkillsEventBusSubscriber skillsEventBusSubscriber,
            IJobPositionsEventBusSubscriber jobPositionsEventBusSubscriber,
            IOptions<PrivateStorageAccountConfigurations> privateStorageConfigurations,
            ILogger<HostedService> logger)
        {
            _jobCandidatesEventBusSubscriber = jobCandidatesEventBusSubscriber;
            _schedulerEventBusSubscriber = schedulerEventBusSubscriber;
            _candidatesEventBusSubscriber = candidatesEventBusSubscriber;
            _skillsEventBusSubscriber = skillsEventBusSubscriber;
            _jobPositionsEventBusSubscriber = jobPositionsEventBusSubscriber;
            _privateStorageConfigurations = privateStorageConfigurations.Value;
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting Event bus CandidateService subscribers");
            await Task.WhenAll(
                _jobCandidatesEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _schedulerEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _candidatesEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _skillsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.RegisterSubscriptionClientAsync(),
                StorageTableHelper.CreateIfNotExistAsync(
                    _privateStorageConfigurations.ConnectionString,
                    FileCacheTableStorage.TableName)
            );
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("Stopping Event bus CandidateService subscribers");
            await Task.WhenAll(
                _jobCandidatesEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _schedulerEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _candidatesEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _skillsEventBusSubscriber.UnRegisterSubscriptionClientAsync(),
                _jobPositionsEventBusSubscriber.UnRegisterSubscriptionClientAsync()
            );

            await Task.WhenAll(
                _jobCandidatesEventBusSubscriber.CloseAsync(),
                _schedulerEventBusSubscriber.CloseAsync(),
                _candidatesEventBusSubscriber.CloseAsync(),
                _skillsEventBusSubscriber.CloseAsync(),
                _jobPositionsEventBusSubscriber.CloseAsync()
            );       
            _logger.LogWarning($"Stopped Event bus CandidateService subscribers");
        }
    }
}
