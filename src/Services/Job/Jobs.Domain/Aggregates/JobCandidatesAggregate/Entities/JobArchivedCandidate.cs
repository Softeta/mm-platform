using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities
{
    public class JobArchivedCandidate : JobCandidateBase
    {
        public ArchivedCandidateStage Stage { get; private set; }

        private JobArchivedCandidate() { }

        public JobArchivedCandidate(
            Guid jobId,
            Guid candidateId,
            string firstName,
            string lastName,
            string? email,
            string? phoneNumber,
            string? pictureUri,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            SystemLanguage? systemLanguage,
            ArchivedCandidateStage stage,
            string? brief,
            DateTimeOffset? invitedAt,
            bool hasApplied)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            CandidateId = candidateId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            PictureUri = pictureUri;
            Position = Position.Create(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            SystemLanguage = systemLanguage;
            Stage = stage;
            Brief = brief;
            InvitedAt = invitedAt;
            HasApplied = hasApplied;
            CreatedAt = DateTimeOffset.UtcNow;

            Validate();
        }

        private void Validate()
        {
            if (CandidateId == Guid.Empty)
            {
                throw new DomainException($"{nameof(CandidateId)} is mandatory",
                    ErrorCodes.Job.ArchivedCandidate.CandidateIdRequired);
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new DomainException($"{nameof(FirstName)} is mandatory",
                    ErrorCodes.Job.ArchivedCandidate.FirstNameRequired);
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new DomainException($"{nameof(LastName)} is mandatory",
                    ErrorCodes.Job.ArchivedCandidate.LastNameRequired);
            }
        }
    }
}
