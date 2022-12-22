using EventBus.EventHandlers;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobsUnshortlistedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateJobsUnshortlistedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateJobsUnshortlistedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateJobsUnshortlistedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var candidateJobs = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();

            var candidateId = candidateJobs.CandidateId;

            foreach (var selectedInJob in candidateJobs.SelectedInJobs)
            {
                var jobCandidates = await jobCandidatesRepository.GetAsync(selectedInJob.JobId);
                jobCandidates.SyncIsSelectedCandidateShortlistedInOtherJob(candidateId, false);
                jobCandidatesRepository.Update(jobCandidates);
            }

            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
