namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Candidate
{
    internal record WorkLocation(
        string Country,
        string? City,
        string? FullAddress,
        DateTimeOffset CreatedAt);
}
