using EventBus.EventHandlers;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.CandidateJobs
{
    public class CandidateJobRejectedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<CandidateJobRejectedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public CandidateJobRejectedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<CandidateJobRejectedIntegrationEvent>(message);

            if (@event?.Payload is null)
            {
                return false;
            }

            var payload = @event.Payload;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();

            var jobCandidates = await jobCandidatesRepository.GetAsync(payload.JobId);

            var jobArchivedCandidateStage = payload.ArchivedInJobs.FirstOrDefault(x => x.JobId == payload.JobId)?.Stage;

            if (jobArchivedCandidateStage is null)
            {
                return false;
            }

            jobCandidates.SyncJobRejection(payload.CandidateId, jobArchivedCandidateStage.Value);

            jobCandidatesRepository.Update(jobCandidates);
            return await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
        }
    }
}
