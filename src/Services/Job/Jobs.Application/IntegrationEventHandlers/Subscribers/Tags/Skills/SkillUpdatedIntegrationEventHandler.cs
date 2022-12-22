using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills.Payloads;
using Jobs.Application.Queries.Jobs;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Infrastructure.Persistence.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills
{
    public class SkillUpdatedIntegrationEventHandler :
        IntegrationEventHandler,
        IIntegrationEventHandler<SkillUpdatedIntegrationEvent>
    {
        private readonly IServiceProvider _serviceProvider;

        public SkillUpdatedIntegrationEventHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override async Task<bool> ExecuteAsync(string message)
        {
            var @event = JsonConvert.DeserializeObject<SkillUpdatedIntegrationEvent>(message);

            ValidatePayload(@event?.Payload);

            var skill = @event!.Payload!;

            await using var scope = _serviceProvider.CreateAsyncScope();
            var jobRepository = scope.ServiceProvider.GetRequiredService<IJobRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SkillUpdatedIntegrationEventHandler>>();

            var jobIds = await mediator.Send(new GetJobsBySkillQuery(skill.Id!.Value));

            foreach (var jobId in jobIds)
            {
                try
                {
                    var job = await jobRepository.GetAsync(jobId);
                    job.SyncSkill(skill.Id!.Value, skill.AliasTo?.Id, skill.AliasTo?.Code);

                    jobRepository.Update(job);
                    await jobRepository.UnitOfWork.SaveEntitiesAsync<Job>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync skill for job: {jobId}. Payload: {message}", jobId, message);
                }
            }

            return true;
        }

        private void ValidatePayload(SkillPayload? payload)
        {
            if (payload is null) throw new Exception("Payload is empty");
            if (payload.Id is null) throw new Exception("Skill id was not found");
        }
    }
}
