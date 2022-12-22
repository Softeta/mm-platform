using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Jobs
{
    internal class JobCandidatesShortlistedIntegrationEventHandler : 
        IntegrationEventHandler,
        IIntegrationEventHandler<JobCandidatesShortlistedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public JobCandidatesShortlistedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobCandidatesShortlistedIntegrationEvent>(message);

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
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            foreach (var candidateId in candidateIds)
            {
                var candidateJobs = await candidateJobsRepository.GetAsync(candidateId);

                if (candidateJobs is null) continue;

                candidateJobs.SyncJobStage(job.JobId, job.Stage);

                candidateJobsRepository.Update(candidateJobs);
            }

            return await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
        }
    }
}
