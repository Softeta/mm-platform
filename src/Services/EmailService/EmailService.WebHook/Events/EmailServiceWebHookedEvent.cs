using Domain.Seedwork;
using EmailService.WebHook.IntegrationEventHandlers.Publishers.Payloads;
using System;

namespace EmailService.WebHook.Events
{
    public class EmailServiceWebHookedEvent : Event
    {
        public EmailServiceWebHookedEvent(EmailServiceWebHookPayload webHook, DateTimeOffset emittedAt, string eventName)
        {
            WebHook = webHook;
            EmittedAt = emittedAt;
            EventName = eventName;
        }

        public EmailServiceWebHookPayload WebHook { get; }
        public DateTimeOffset EmittedAt { get; }
        public string EventName { get; }
    }
}
