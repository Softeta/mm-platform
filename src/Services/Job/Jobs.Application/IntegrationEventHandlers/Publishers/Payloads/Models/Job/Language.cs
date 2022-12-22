namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public record Language(Guid Id, string Code, string Name, DateTimeOffset CreatedAt);
}
