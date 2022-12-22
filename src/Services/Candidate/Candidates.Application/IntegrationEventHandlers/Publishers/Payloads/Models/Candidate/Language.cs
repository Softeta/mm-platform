namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal record Language(Guid Id, string Code, string Name, DateTimeOffset CreatedAt);
}
