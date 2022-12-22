using Candidates.Domain.Aggregates.CandidateJobsAggregate.ValueObjects;
using Domain.Seedwork;
using Domain.Seedwork.Attributes;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Extensions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities
{
    public abstract class JobInformationBase : Entity
    {
        [ProjectionField]
        public Guid JobId { get; init; }

        [ProjectionField]
        public JobStage JobStage { get; protected set; }

        [ProjectionField]
        public Position Position { get; protected set; } = null!;

        [ProjectionField]
        public Guid CandidateId { get; init; }

        [ProjectionField]
        public Company Company { get; protected set; } = null!;

        [ProjectionField]
        public WorkType? Freelance { get; protected set; }

        [ProjectionField]
        public WorkType? Permanent { get; protected set; }

        [ProjectionField]
        public DateTimeOffset? StartDate { get; protected set; }

        [ProjectionField]
        public DateTimeOffset? DeadlineDate { get; protected set; }

        [ProjectionField]
        public bool IsJobArchived { get; protected set; }

        public virtual void SyncJobInformation(
            JobStage jobStage,
            Guid positionId,
            string positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Guid companyId,
            string companyName,
            string? companyLogo,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? startDate,
            DateTimeOffset? deadlineDate)
        {
            ChangeJobStage(jobStage);
            Freelance = freelance;
            Permanent = permanent;
            StartDate = startDate;
            DeadlineDate = deadlineDate;
            Position = new Position(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            Company = new Company(companyId, companyName, companyLogo);
        }

        public void ChangeJobStage(JobStage stage)
        {
            JobStage = stage;
            IsJobArchived = stage.IsArchived();
        }

        public void SyncJobPosition(Guid? aliasId, string? aliasCode)
        {
            Position = new Position(Position.Id, Position.Code, aliasId, aliasCode);
        }
    }
}
