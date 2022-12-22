using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills.Payloads;
using Candidates.Application.Queries;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Infrastructure.Persistence.Repositories;
using EventBus.EventHandlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills
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
            var candidateRepository = scope.ServiceProvider.GetRequiredService<ICandidateRepository>();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SkillUpdatedIntegrationEventHandler>>();

            var candidateIds = await mediator.Send(new GetCandidatesBySkillQuery(skill.Id!.Value));

            foreach (var candidateId in candidateIds)
            {
                try
                {
                    var candidate = await candidateRepository.GetAsync(candidateId);
                    candidate.SyncSkills(skill.Id!.Value, skill.AliasTo?.Id, skill.AliasTo?.Code);

                    candidateRepository.Update(candidate);
                    await candidateRepository.UnitOfWork.SaveEntitiesAsync<Candidate>();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to sync skill for candidate: {candidateId}. Payload: {message}", candidateId, message);
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
