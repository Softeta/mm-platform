using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.CandidateJobs;
using Candidates.Application.IntegrationEventHandlers.Publishers.Payloads.Models.Shared;
using Candidates.Domain.Aggregates.CandidateJobsAggregate;
using DomainCandidate = Candidates.Domain.Aggregates.CandidateAggregate.Candidate;

namespace Candidates.Application.IntegrationEventHandlers.Publishers.Payloads
{
    internal class CandidateAppliedToJobPayload : CandidateJobChangedPayload
    {
        public Candidate Candidate { get; set; } = null!;

        public static CandidateAppliedToJobPayload FromEvent(
            CandidateJobs candidateJobs,
            Guid jobId,
            DomainCandidate candidate)
        {
            return new CandidateAppliedToJobPayload
            {
                CandidateId = candidateJobs.Id,
                JobId = jobId,
                IsShortlisted = candidateJobs.IsShortlisted,
                SelectedInJobs = candidateJobs.SelectedInJobs.Select(CandidateSelectedInJob.FromDomain),
                ArchivedInJobs = candidateJobs.ArchivedInJobs.Select(CandidateArchivedInJob.FromDomain),
                AppliedInJobs = candidateJobs.AppliedInJobs.Select(CandidateAppliedInJob.FromDomain),
                Candidate = new Candidate
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    Email = candidate.Email?.Address,
                    PhoneNumber = candidate.Phone?.PhoneNumber,
                    PictureUri = candidate.Picture?.ThumbnailUri,
                    Position = Position.FromDomainNullable(candidate.CurrentPosition),
                    SystemLanguage = candidate.SystemLanguage
                }
            };
        }
    }
}
