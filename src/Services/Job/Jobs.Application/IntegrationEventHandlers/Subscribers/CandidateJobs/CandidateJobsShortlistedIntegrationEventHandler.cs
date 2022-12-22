using Domain.Seedwork.Extensions;
using EventBus.EventHandlers;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobsShortlistedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateJobsShortlistedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateJobsShortlistedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateJobsShortlistedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var candidateJobs = @event.Payload;

            var shortlistedInJobId = candidateJobs.SelectedInJobs
                .Where(x => x.Stage.IsShortlisted())
                .FirstOrDefault()?.JobId;

            if (shortlistedInJobId is null) 
            {
                return true;
            } 

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();

            var candidateId = candidateJobs.CandidateId;

            foreach (var selectedInJob in candidateJobs.SelectedInJobs)
            {
                if (shortlistedInJobId != selectedInJob.JobId)
                {
                    var jobCandidates = await jobCandidatesRepository.GetAsync(selectedInJob.JobId);
                    jobCandidates.SyncIsSelectedCandidateShortlistedInOtherJob(candidateId, true);
                    jobCandidatesRepository.Update(jobCandidates);
                }
            }

            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
