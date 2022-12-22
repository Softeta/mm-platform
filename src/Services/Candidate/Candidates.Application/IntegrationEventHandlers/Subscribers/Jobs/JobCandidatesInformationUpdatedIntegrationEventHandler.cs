using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    public class JobCandidatesInformationUpdatedIntegrationEventHandler :
        IntegrationEventHandler, 
        IIntegrationEventHandler<JobCandidatesInformationUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobCandidatesInformationUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobCandidatesInformationUpdatedIntegrationEvent>(message);

            if (@event?.Payload is null) return false;

            var job = @event.Payload;

            var candidateIds = @event
                .Payload
                .SelectedCandidates
                .Select(x => x.CandidateId)
                .Concat(@event
                    .Payload
                    .ArchivedCandidates
                    .Select(x => x.CandidateId));

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();

            foreach (var candidateId in candidateIds)
            {
                var candidateJobs = await candidateJobsRepository.GetAsync(candidateId);

                if (candidateJobs is null) continue;

                candidateJobs.SyncJobInformation(
                    job.JobId,
                    job.Stage,
                    job.Position.Id,
                    job.Position.Code,
                    job.Position.AliasTo?.Id,
                    job.Position.AliasTo?.Code,
                    job.Company.Id,
                    job.Company.Name,
                    job.Company.LogoUri,
                    job.Freelance,
                    job.Permanent,
                    job.StartDate,
                    job.DeadlineDate);

                candidateJobsRepository.Update(candidateJobs);
            }

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }
    }
}
