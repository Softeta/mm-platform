using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities
{
    public class CandidateAppliedInJob : JobInformationBase
    {

        private CandidateAppliedInJob() { }

        public CandidateAppliedInJob(
            Guid jobId,
            JobStage jobStage,
            Guid positionId,
            string positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Guid candidateId,
            Guid companyId,
            string companyName,
            string? companyLogo,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? invitedAt)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            ChangeJobStage(jobStage);
            Position = new Position(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            CandidateId = candidateId;
            Company = new Company(companyId, companyName, companyLogo);
            Freelance = freelance;
            Permanent = permanent;
            StartDate = startDate;
            DeadlineDate = deadlineDate;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
