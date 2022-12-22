using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions.Payloads;
using Jobs.Application.Queries.Jobs;
using Jobs.Application.Queries.JobsCandidates;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.JobPositions
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
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var jobCandidatesRepository = scope.ServiceProvider.GetRequiredService<IJobCandidatesRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<JobPositionUpdatedIntegrationEventHandler>>();

            var jobIds = await mediator.Send(new GetJobsByPositionQuery(jobPosition.Id!.Value));

            foreach (var jobId in jobIds)
            {
                try
                {
                    var job = await jobRepository.GetAsync(jobId);
                    job.SyncJobPositions(jobPosition.Id!.Value, jobPosition.AliasTo?.Id, jobPosition.AliasTo?.Code);

                    jobRepository.Update(job);
                    await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync job position for job: {jobId}. Payload: {message}", jobId, message);
                }
            }

            var jobCandidatesIds = await mediator.Send(new GetJobCandidatesByJobPositionQuery(jobPosition.Id!.Value));

            foreach (var jobCandidatesId in jobCandidatesIds)
            {
                try
                {
                    var jobCandidates = await jobCandidatesRepository.GetAsync(jobCandidatesId);
                    jobCandidates.SyncJobPositions(jobPosition.Id!.Value, jobPosition.AliasTo?.Id, jobPosition.AliasTo?.Code);

                    jobCandidatesRepository.Update(jobCandidates);
                    await jobCandidatesRepository.UnitOfWork.SaveEntitiesAsync<JobCandidates>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync job position for job candidates: {jobCandidatesId}. Payload: {message}", jobCandidatesId, message);
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
