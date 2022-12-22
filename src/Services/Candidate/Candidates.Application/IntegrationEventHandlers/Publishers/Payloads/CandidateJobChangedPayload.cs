using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class CandidateJobChangedPayload : CandidateJobsPayload
    {
        public Guid JobId { get; set; }

        public static CandidateJobChangedPayload FromDomain(CandidateJobs candidateJobs, Guid jobId)
        {
            return new CandidateJobChangedPayload
            {
                CandidateId = candidateJobs.Id,
                JobId = jobId,
                IsShortlisted = candidateJobs.IsShortlisted,
                SelectedInJobs = candidateJobs.SelectedInJobs.Select(CandidateSelectedInJob.FromDomain),
                ArchivedInJobs = candidateJobs.ArchivedInJobs.Select(CandidateArchivedInJob.FromDomain),
                AppliedInJobs = candidateJobs.AppliedInJobs.Select(CandidateAppliedInJob.FromDomain)
            };
        }
    }
}
