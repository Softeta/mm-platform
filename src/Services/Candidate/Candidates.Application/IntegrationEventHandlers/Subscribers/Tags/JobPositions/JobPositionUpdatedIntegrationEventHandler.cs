using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;
using Candidates.Application.Queries;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Positions
{
    public class JobPositionUpdatedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<JobPositionUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;
        public JobPositionUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<JobPositionUpdatedIntegrationEvent>(message);

            ValidatePayload(@event?.Payload);

            var jobPosition = @event!.Payload!;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var candidateRepository = scope.ServiceProvider.GetRequiredService<ICandidateRepository>();
            var candidateJobsRepository = scope.ServiceProvider.GetRequiredService<ICandidateJobsRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<JobPositionUpdatedIntegrationEventHandler>>();

            var candidateIds = await mediator.Send(new GetCandidatesByJobPositionQuery(jobPosition.Id!.Value));

            foreach (var candidateId in candidateIds)
            {
                try
                {
                    var candidate = await candidateRepository.GetAsync(candidateId);
                    candidate.SyncJobPositions(jobPosition.Id!.Value, jobPosition.AliasTo?.Id, jobPosition.AliasTo?.Code);

                    candidateRepository.Update(candidate);
                    await candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync job position for candidate: {candidateId}. Payload: {message}", candidateId, message);
                }
            }

            var candidateJobsIds = await mediator.Send(new GetCandidateJobsByJobPositionQuery(jobPosition.Id!.Value));

            foreach (var candidateJobsId in candidateJobsIds)
            {
                try
                {
                    var candidateJobs = await candidateJobsRepository.GetAsync(candidateJobsId);
                    if (candidateJobs is not null)
                    {
                        candidateJobs.SyncJobPositions(jobPosition.Id!.Value, jobPosition.AliasTo?.Id, jobPosition.AliasTo?.Code);
                        candidateJobsRepository.Update(candidateJobs);
                        await candidateJobsRepository.UnitOfWork.SaveEntitiesAsync<CandidateJobs>();
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync job position for candidate jobs: {candidateJobsId}. Payload: {message}", candidateJobsId, message);
                }
            }
            return true;
        }

        private void ValidatePayload(JobPositionPayload? payload)
        {
            if (payload is null) throw new Exception("Payload is empty");
            if (payload.Id is null) throw new Exception("Job position id was not found");
        }
    }
}
