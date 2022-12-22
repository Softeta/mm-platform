using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities
{
    public class CandidateArchivedInJob : CandidateInJobBase
    {
        public ArchivedCandidateStage Stage { get; private set; }

        private CandidateArchivedInJob() { }

        public CandidateArchivedInJob(
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
            ArchivedCandidateStage stage,
            DateTimeOffset? invitedAt,
            bool hasApplied)
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
            Stage = stage;
            Freelance = freelance;
            Permanent = permanent;
            StartDate = startDate;
            DeadlineDate = deadlineDate;
            InvitedAt = invitedAt;
            HasApplied = hasApplied;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Sync(ArchivedCandidateStage stage, DateTimeOffset? invitedAt, bool hasApplied)
        {
            Stage = stage;
            SyncCandidateJobInformation(invitedAt, hasApplied);
        }
    }
}
