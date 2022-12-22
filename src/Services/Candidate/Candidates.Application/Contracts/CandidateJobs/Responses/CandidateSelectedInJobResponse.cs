using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Contracts.Candidate.CandidateJobs.Responses;
using Persistence.Customization.FileStorage.Clients.Private;
using SharedContracts = Contracts.Shared;

namespace Candidates.Application.Contracts.CandidateJobs.Responses
{
    public class CandidateSelectedInJobResponse : GetCandidateSelectedInJobResponse
    {
        public static CandidateSelectedInJobResponse FromDomain(
            CandidateSelectedInJob candidateSelectedInJob,
            IPrivateBlobClient privateBlobClient)
        {
            return new CandidateSelectedInJobResponse
            {
                Id = candidateSelectedInJob.Id,
                JobId = candidateSelectedInJob.JobId,
                CandidateId = candidateSelectedInJob.CandidateId,
                Company = CompanyResponse.FromDomain(candidateSelectedInJob.Company),
                Position = SharedContracts.Position.FromDomainNotNullable(candidateSelectedInJob.Position),
                JobStage = candidateSelectedInJob.JobStage,
                Freelance = candidateSelectedInJob.Freelance,
                Permanent = candidateSelectedInJob.Permanent,
                StartDate = candidateSelectedInJob.StartDate,
                DeadlineDate = candidateSelectedInJob.DeadlineDate,
                MotivationVideo = DocumentResponse.FromDomain(candidateSelectedInJob.MotivationVideo, privateBlobClient),
                CoverLetter = candidateSelectedInJob.CoverLetter,
                Stage = candidateSelectedInJob.Stage,
                IsJobArchived = candidateSelectedInJob.IsJobArchived,
                HasApplied = candidateSelectedInJob.HasApplied
            };
        }
    }
}
