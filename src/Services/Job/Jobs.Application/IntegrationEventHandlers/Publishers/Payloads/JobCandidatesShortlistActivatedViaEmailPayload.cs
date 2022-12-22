using Domain.Seedwork.Enums;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads
{
    public class JobCandidatesShortlistActivatedViaEmailPayload
    {
        public JobCandidatesPayload JobCandidates { get; set; } = null!;
        public string ContactEmail { get; set; } = null!;
        public string? ContactFirstName { get; set; }
        public SystemLanguage? ContactSystemLanguage { get; set; }
        public Guid? ContactExternalId { get; set; }
    }
}
