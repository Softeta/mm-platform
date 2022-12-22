using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs
{
    internal abstract class CandidateInJob
    {
        public Guid JobId { get; set; }
        public Position Position { get; set; } = null!;
        public Guid CandidateId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
