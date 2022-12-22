using Domain.Seedwork.Enums;
using EventBus.EventHandlers;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobsHiredIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateJobsHiredIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateJobsHiredIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateJobsHiredIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var candidateJobs = @event.Payload;

            var hiredInJobId = candidateJobs.SelectedInJobs
                .FirstOrDefault(x => x.Stage == SelectedCandidateStage.Hired)?.JobId;

            if (hiredInJobId is null) 
            {
                return true;
            } 

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();

            var candidateId = candidateJobs.CandidateId;

            foreach (var selectedInJob in candidateJobs.SelectedInJobs)
            {
                if (hiredInJobId == selectedInJob.JobId)
                {
                    continue;
                }

                var jobCandidates = await jobCandidatesRepository.GetAsync(selectedInJob.JobId);
                jobCandidates.SyncIsSelectedCandidateHiredInOtherJob(candidateId, true);
                jobCandidatesRepository.Update(jobCandidates);
            }

            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
