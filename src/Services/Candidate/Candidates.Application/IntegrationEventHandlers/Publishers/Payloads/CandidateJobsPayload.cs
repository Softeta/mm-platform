using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class CandidateJobsPayload
    {
        public Guid CandidateId { get; set; }
        public bool IsShortlisted { get; set; }
        public IEnumerable<CandidateSelectedInJob>? SelectedInJobs { get; set; }
        public IEnumerable<CandidateArchivedInJob>? ArchivedInJobs { get; set; }
        public IEnumerable<CandidateAppliedInJob>? AppliedInJobs { get; set; }

        public static CandidateJobsPayload FromDomain(CandidateJobs candidateJobs)
        {
            return new CandidateJobsPayload
            {
                CandidateId = candidateJobs.Id,
                IsShortlisted = candidateJobs.IsShortlisted,
                SelectedInJobs = candidateJobs.SelectedInJobs.Select(CandidateSelectedInJob.FromDomain),
                ArchivedInJobs = candidateJobs.ArchivedInJobs.Select(CandidateArchivedInJob.FromDomain),
                AppliedInJobs = candidateJobs.AppliedInJobs.Select(CandidateAppliedInJob.FromDomain)
            };
        }
    }
}
