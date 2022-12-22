using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities
{
    public class CandidateSelectedInJob : CandidateInJobBase
    {
        public SelectedCandidateStage Stage { get; private set; }

        private CandidateSelectedInJob() { }

        public CandidateSelectedInJob(
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
            SelectedCandidateStage stage,
            DateTimeOffset? invitedAt,
            bool hasApplied)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            ChangeJobStage(jobStage);
            CandidateId = candidateId;
            Position = new Position(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
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

        public void Sync(SelectedCandidateStage stage, DateTimeOffset? invitedAt, bool hasApplied)
        {
            Stage = stage;
            SyncCandidateJobInformation(invitedAt, hasApplied);
        }

        public void Update(
            string? motivationVideoUri,
            string? motivationVideoFileName,
            bool isMotivationVideoChanged,
            string? coverLetter)
        {
            CoverLetter = coverLetter;

            if (isMotivationVideoChanged)
            {
                MotivationVideo = new Document(motivationVideoUri, motivationVideoFileName);
            }
        }
    }
}
