using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Job
{
    public record Seniority(SeniorityLevel SeniorityLevel, DateTimeOffset CreatedAt);
}
