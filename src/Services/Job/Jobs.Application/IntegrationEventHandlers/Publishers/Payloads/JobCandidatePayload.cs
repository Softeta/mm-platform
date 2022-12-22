using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads
{
    public class JobCandidatePayload : Candidate
    {
        public Job Job { get; set; } = null!;

        public static JobCandidatePayload FromDomain(JobCandidates jobCandidates, JobCandidateBase candidate)
        {
            return new JobCandidatePayload
            {
                Job = Job.FromDomain(jobCandidates),
                CandidateId = candidate.CandidateId,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                PictureUri = candidate.PictureUri,
                InvitedAt = candidate.InvitedAt,
                SystemLanguage = candidate.SystemLanguage
            };
        }
    }
}
