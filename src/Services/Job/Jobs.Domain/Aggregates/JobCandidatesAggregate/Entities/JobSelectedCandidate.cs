using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Extensions;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities
{
    public class JobSelectedCandidate : JobCandidateBase
    {
        public SelectedCandidateStage Stage { get; private set; }
        public int? Ranking { get; private set; }
        public bool IsShortListedInOtherJob { get; private set; }
        public bool IsHiredInOtherJob { get; private set; }
        public bool IsShortListed => Stage.IsShortlisted();
        public bool IsHired => Stage == SelectedCandidateStage.Hired;

        private JobSelectedCandidate() { }

        public JobSelectedCandidate(
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
            bool isShortListedInOtherJob,
            bool isHiredInOtherJob,
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
            Brief = brief;
            InvitedAt = invitedAt;
            SystemLanguage = systemLanguage;
            Stage = SelectedCandidateStage.New;
            IsShortListedInOtherJob = isShortListedInOtherJob;
            IsHiredInOtherJob = isHiredInOtherJob;
            HasApplied = hasApplied;
            CreatedAt = DateTime.UtcNow;

            Validate();
        }

        public void UpdateStage(SelectedCandidateStage stage, int? ranking = null)
        {
            Ranking = ranking;
            Stage = stage;
        }

        public void UpdateIsShortlistedInOtherJob(bool isShortlistedInOtherJob)
        {
            IsShortListedInOtherJob = isShortlistedInOtherJob;
        }

        public void UpdateIsHiredInOtherJob(bool isHiredInOtherJob)
        {
            IsHiredInOtherJob = isHiredInOtherJob;
        }

        public void UpdateRanking(int ranking)
        {
            Ranking = ranking;
            ValidateRanking();
        }

        public void Invite()
        {
            if (Stage == SelectedCandidateStage.New)
            {
                Stage = SelectedCandidateStage.InvitePending;
            }

            InvitedAt = DateTimeOffset.UtcNow;
        }

        public void Apply()
        {
            HasApplied = true;
        }

        private void Validate()
        {
            if (CandidateId == Guid.Empty)
            {
                throw new DomainException($"{nameof(CandidateId)} is mandatory",
                    ErrorCodes.Job.SelectedCandidate.CandidateIdRequired);
            }
            if (string.IsNullOrWhiteSpace(FirstName))
            {
                throw new DomainException($"{nameof(FirstName)} is mandatory",
                    ErrorCodes.Job.SelectedCandidate.FirstNameRequired);
            }
            if (string.IsNullOrWhiteSpace(LastName))
            {
                throw new DomainException($"{nameof(LastName)} is mandatory",
                    ErrorCodes.Job.SelectedCandidate.LastNameRequired);
            }
        }

        private void ValidateRanking()
        {
            if (!IsShortListed)
            {
                throw new DomainException(
                    $"Ranking update is not allowed, when status: {Stage}",
                    ErrorCodes.Job.SelectedCandidate.RankingUpdateNotAllowed);
            }
            if (Ranking < 1)
            {
                throw new DomainException("Ranking must be 1 or higher", 
                    ErrorCodes.Job.SelectedCandidate.InvalidRankingValue);
            }
        }
    }
}
