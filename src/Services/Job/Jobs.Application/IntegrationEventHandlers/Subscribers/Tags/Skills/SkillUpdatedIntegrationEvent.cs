using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Tags.Skills
{
    public class SkillUpdatedIntegrationEvent : IntegrationEvent
    {
        public SkillPayload? Payload { get; set; }
    }
}
