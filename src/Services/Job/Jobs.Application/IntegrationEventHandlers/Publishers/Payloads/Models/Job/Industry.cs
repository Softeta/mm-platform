namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public record Industry(Guid IndustryId, string Code, DateTimeOffset CreatedAt);
}
