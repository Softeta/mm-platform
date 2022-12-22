using Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills.Payloads;
using EventBus.EventHandlers;

namespace Candidates.Application.IntegrationEventHandlers.Subscribers.Tags.Skills
{
    public class SkillUpdatedIntegrationEvent : IntegrationEvent
    {
        public SkillPayload? Payload { get; set; }
    }
}
