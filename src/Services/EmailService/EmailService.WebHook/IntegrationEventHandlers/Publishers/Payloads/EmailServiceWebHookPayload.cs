using System;

namespace EmailService.WebHook.IntegrationEventHandlers.Publishers.Payloads
{
    public class EmailServiceWebHookPayload
    {
        public Guid EntityId { get; set; }
        public Guid TargetId { get; set; }
        public string Email { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTimeOffset Date { get; set; }
    }
}
