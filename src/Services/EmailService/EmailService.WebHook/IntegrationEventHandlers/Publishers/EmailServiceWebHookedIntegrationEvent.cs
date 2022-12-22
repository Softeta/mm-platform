using EmailService.WebHook.IntegrationEventHandlers.Publishers.Payloads;
using EventBus.EventHandlers;
using System;

namespace EmailService.WebHook.IntegrationEventHandlers.Publishers
{
    internal class EmailServiceWebHookedIntegrationEvent : IntegrationEvent
    {
        public EmailServiceWebHookedIntegrationEvent(EmailServiceWebHookPayload payload, DateTimeOffset emittedAt) : base(emittedAt)
        {
            Payload = payload;
        }

        public EmailServiceWebHookPayload Payload { get; }
    }
}
