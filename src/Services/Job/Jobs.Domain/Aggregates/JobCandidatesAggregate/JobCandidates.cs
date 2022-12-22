using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Extensions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.ValueObjects;
using Jobs.Domain.Events.JobCandidatesAggregate;

namespace Jobs.Domain.Aggregates.JobCandidatesAggregate
{
    public class JobCandidates : AggregateRoot
    {
        public JobStage Stage { get; private set; }
        public Position Position { get; private set; } = null!;
        public Company Company { get; private set; } = null!;
        public WorkType? Freelance { get; private set; }
        public WorkType? Permanent { get; private set; }
        public DateTimeOffset? DeadlineDate { get; private set; }
        public DateTimeOffset? StartDate { get; private set; }
        public DateTimeOffset? ShortListActivatedAt { get; private set; }

        private readonly List<JobSelectedCandidate> _selectedCandidates = new();
        public IReadOnlyList<JobSelectedCandidate> SelectedCandidates => _selectedCandidates;

        private readonly List<JobArchivedCandidate> _archivedCandidates = new();
        public IReadOnlyList<JobArchivedCandidate> ArchivedCandidates => _archivedCandidates;

        private JobCandidates() { }

        public JobCandidates(
            Guid jobId,
            JobStage stage,
            Guid positionId, 
            string positionCode, 
            Guid? positionAliasToId,
            string? positionAliasToCode,
            Guid companyId, 
            string companyName, 
            string? companyLogoUri,
            WorkType? freelance,
            WorkType? permanent, 
            DateTimeOffset? deadlineDate,
            DateTimeOffset? startDate)
        {
            Id = jobId;
            Stage = stage;
            Position = new Position(
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode);
            Company = new Company(companyId, companyName, companyLogoUri);
            Freelance = freelance;
            Permanent = permanent;
            DeadlineDate = deadlineDate;
            StartDate = startDate;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void AddSelectedCandidates(IEnumerable<JobSelectedCandidate> selectedCandidates)
        {
            foreach (var candidate in selectedCandidates)
            {
                if (_selectedCandidates.Any(c => candidate.CandidateId == c.CandidateId))
                {
                    continue;
                }

                _selectedCandidates.Add(candidate);
            }

            AddEvent(new SelectedCandidatesAddedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void HireSelectedCandidates(IEnumerable<Guid> candidateIds)
        {
            if (Stage == JobStage.Successful)
            {
                throw new DomainException($"Job is already Successful",
                    ErrorCodes.Job.Candidate.JobHasHired);
            }
            if (candidateIds.Count() != 1)
            {
                throw new DomainException($"Only one candidate can be hired",
                    ErrorCodes.Job.Candidate.OnlyOneHired);
            }

            var candidateId = candidateIds.First();
            var candidate = GetSelectedCandidate(candidateId);

            if (candidate is null)
            {
                throw new DomainException($"Candidate id {candidateId} does not exist for job id: {Id}",
                    ErrorCodes.Job.Candidate.NotExists);
            }

            candidate.UpdateStage(SelectedCandidateStage.Hired);
            Stage = JobStage.Successful;

            AddEvent(new JobCandidatesHiredDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void UpdateSelectedCandidates(IEnumerable<Guid> candidateIds, SelectedCandidateStage stage)
        {
            foreach (var candidateId in candidateIds)
            {
                var candidate = GetSelectedCandidate(candidateId);

                if (candidate is null)
                {
                    throw new DomainException($"Candidates id {candidateId} does not exist for job id: {Id}",
                        ErrorCodes.Job.Candidate.NotExists);
                }

                var ranking = CalculateCandidateRanking(candidate, stage);
                candidate.UpdateStage(stage, ranking);
            }
            
            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void ArchiveCandidates(IEnumerable<Guid> candidateIds, ArchivedCandidateStage candidateStage)
        {
            foreach (var candidateId in candidateIds)
            {
                ArchiveCandidate(candidateId, candidateStage);
            }

            AddEvent(new ArchivedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void ActivateArchivedCandidates(IEnumerable<(Guid CandidateId, bool IsShortListedInOtherJob, bool IsHiredInOtherJob)> candidateIds)
        {
            foreach (var (candidateId, isShortListedInOtherJob, isHiredInOtherJob) in candidateIds)
            {
                var archivedCandidate = GetArchivedCandidate(candidateId);

                if (archivedCandidate is null)
                {
                    throw new DomainException($"Candidate with id {candidateId} does not exist in archived candidates collection for job id: {Id}",
                        ErrorCodes.Job.Candidate.NotExistsInArchivedCandidates);
                }

                var selectedCandidate = new JobSelectedCandidate(
                    Id,
                    candidateId,
                    archivedCandidate.FirstName,
                    archivedCandidate.LastName,
                    archivedCandidate.Email,
                    archivedCandidate.PhoneNumber,
                    archivedCandidate.PictureUri,
                    archivedCandidate.Position?.Id,
                    archivedCandidate.Position?.Code,
                    archivedCandidate.Position?.AliasTo?.Id,
                    archivedCandidate.Position?.AliasTo?.Code,
                    archivedCandidate.SystemLanguage,
                    isShortListedInOtherJob,
                    isHiredInOtherJob,
                    archivedCandidate.Brief,
                    archivedCandidate.InvitedAt,
                    archivedCandidate.HasApplied);


                _selectedCandidates.Add(selectedCandidate);
                _archivedCandidates.Remove(archivedCandidate);
            }

            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncCandidateProfile(
            Guid candidateId,
            string firstName,
            string lastName,
            string email,
            string? phoneNumber,
            string? pictureUri,
            Guid? positionId,
            string? positionCode,
            Guid? positionAliasToId,
            string? positionAliasToCode,
            SystemLanguage? systemLanguage)
        {
            var selectedCandidate = GetJobCandidate(candidateId);

            if (selectedCandidate is not null)
            {
                selectedCandidate.Sync(
                    firstName,
                    lastName,
                    email,
                    phoneNumber,
                    pictureUri,
                    positionId,
                    positionCode,
                    positionAliasToId,
                    positionAliasToCode,
                    systemLanguage);
            }
        }

        public void SyncIsSelectedCandidateShortlistedInOtherJob(Guid candidateId, bool isShortlistedInOtherJob)
        {
            var selectedCandidate = GetSelectedCandidate(candidateId);

            if (selectedCandidate is null) return;

            selectedCandidate.UpdateIsShortlistedInOtherJob(isShortlistedInOtherJob);
        }

        public void SyncIsSelectedCandidateHiredInOtherJob(Guid candidateId, bool isHiredInOtherJob)
        {
            var selectedCandidate = GetSelectedCandidate(candidateId);

            selectedCandidate?.UpdateIsHiredInOtherJob(isHiredInOtherJob);
        }

        public void ShareShortlistViaEmail(
            string contactEmail,
            string? contactFirstName,
            SystemLanguage? contactSystemLanguage,
            Guid? contactExternalId)
        {
            Shorlist();
            AddEvent(new JobCandidatesSharedShortlistViaEmailDomainEvent(
                this,
                contactEmail,
                contactFirstName,
                contactSystemLanguage,
                contactExternalId,
                ShortListActivatedAt!.Value));
        }

        public void ShareShortlistViaLink()
        {
            Shorlist();
        }

        public void PublishSelectedCandidatesUpdatedEvent()
        {
            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void PublishArchivedCandidatesUpdatedEvent()
        {
            AddEvent(new ArchivedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncJobStage(JobStage jobStage)
        {
            Stage = jobStage;
            
            AddEvent(new JobCandidatesJobStageUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncJob(
            Position position,
            WorkType? freelance,
            WorkType? permanent,
            DateTimeOffset? deadlineDate,
            DateTimeOffset? startDate)
        {
            Position = position;
            Freelance = freelance;
            Permanent = permanent;
            DeadlineDate = deadlineDate;
            StartDate = startDate;

            AddEvent(new JobCandidatesInformationUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void UpdateSelectedCandidatesRanking(IEnumerable<(Guid Id, int Ranking)> rankings)
        {
            ValidateRankings(rankings);

            foreach (var ranking in rankings)
            {
                var selectedCandidate = GetSelectedCandidate(ranking.Id);

                if (selectedCandidate is not null)
                {
                    selectedCandidate.UpdateRanking(ranking.Ranking);
                }
            }
        }

        public void UpdateBrief(Guid candidateId, string? brief)
        {
            var selectedCandidate = GetSelectedCandidate(candidateId);
            if (selectedCandidate is not null)
            {
                selectedCandidate.UpdateBrief(brief);
                return;
            }

            var archivedCandidate = GetArchivedCandidate(candidateId);
            if (archivedCandidate is not null)
            {
                archivedCandidate.UpdateBrief(brief);
                return;
            }

            throw new DomainException($"Job candidate does not exist", ErrorCodes.Job.Candidate.NotExists);
        }

        public void InviteSelectedCandidatesViaEmail(IEnumerable<Guid> candidateIds)
        {
            foreach (var candidateId in candidateIds)
            {
                var selectedCandidate = GetSelectedCandidate(candidateId);

                if (selectedCandidate is null)
                {
                    continue;
                }
                if (string.IsNullOrWhiteSpace(selectedCandidate.Email))
                {
                    continue;
                }
                selectedCandidate.Invite();
                AddEvent(new JobSelectedCandidateInvitedDomainEvent(this, selectedCandidate, DateTimeOffset.UtcNow));
            }

            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void InviteSelectedCandidatesViaLink(IEnumerable<Guid> candidateIds)
        {
            foreach (var candidateId in candidateIds)
            {
                var selectedCandidate = GetSelectedCandidate(candidateId);
                if (selectedCandidate is null)
                {
                    continue;
                }
                selectedCandidate.Invite();
            }

            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncJobRejection(Guid candidateId, ArchivedCandidateStage candidateStage)
        {
            ArchiveCandidate(candidateId, candidateStage);
        }

        public void SyncApplyToJob(
            Guid candidateId,
            string candidateFirstName,
            string candidateLastName,
            string? candidateEmail,
            string? candidatePhoneNumber,
            string? candidatePictureUri,
            Position? candidatePosition,
            SystemLanguage? candidateSystemLanguage)
        {
            var selectedCandidate = GetSelectedCandidate(candidateId);

            if (selectedCandidate is null)
            {
                selectedCandidate = new JobSelectedCandidate(
                    Id,
                    candidateId,
                    candidateFirstName,
                    candidateLastName,
                    candidateEmail,
                    candidatePhoneNumber,
                    candidatePictureUri,
                    candidatePosition?.Id,
                    candidatePosition?.Code,
                    candidatePosition?.AliasTo?.Id,
                    candidatePosition?.AliasTo?.Code,
                    candidateSystemLanguage,
                    false,
                    false,
                    null,
                    null,
                    true);
                _selectedCandidates.Add(selectedCandidate);
            }
            else
            {
                if (selectedCandidate.Stage == SelectedCandidateStage.InvitePending &&
                    selectedCandidate.InvitedAt.HasValue)
                {
                    selectedCandidate.UpdateStage(SelectedCandidateStage.Interested);
                }
                selectedCandidate.Apply();
            }

            AddEvent(new SelectedCandidatesUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncJobPositions(Guid id, Guid? aliasId, string? aliasCode)
        {
            if (Position.Id == id)
            {
                Position = new Position(id, Position.Code, aliasId, aliasCode);
            }

            var selectedCandidates = _selectedCandidates.Where(x => x.Position != null && x.Position.Id == id);
            var archivedCandidates = _archivedCandidates.Where(x => x.Position != null && x.Position.Id == id);

            var jobCandidates = new List<JobCandidateBase>()
                .Concat(selectedCandidates)
                .Concat(archivedCandidates);

            foreach (var jobCandidate in jobCandidates)
            {
                jobCandidate.SyncJobPosition(aliasId, aliasCode);
            }
        }

        private void ArchiveCandidate(Guid candidateId, ArchivedCandidateStage candidateStage)
        {
            var selectedCandidate = GetSelectedCandidate(candidateId);

            if (selectedCandidate is null)
            {
                throw new DomainException($"Candidate with id {candidateId} does not exist in selected candidates collection for job id: {Id}",
                    ErrorCodes.Job.Candidate.NotExistsInSelectedCandidates);
            }

            var archivedCandidate = new JobArchivedCandidate(
                Id,
                candidateId,
                selectedCandidate.FirstName,
                selectedCandidate.LastName,
                selectedCandidate.Email,
                selectedCandidate.PhoneNumber,
                selectedCandidate.PictureUri,
                selectedCandidate.Position?.Id,
                selectedCandidate.Position?.Code,
                selectedCandidate.Position?.AliasTo?.Id,
                selectedCandidate.Position?.AliasTo?.Code,
                selectedCandidate.SystemLanguage,
                candidateStage,
                selectedCandidate.Brief,
                selectedCandidate.InvitedAt,
                selectedCandidate.HasApplied);

            _archivedCandidates.Add(archivedCandidate);
            _selectedCandidates.Remove(selectedCandidate);
        }

        private int? CalculateCandidateRanking(JobSelectedCandidate candidate, SelectedCandidateStage stage)
        {
            if (stage.IsShortlisted() && !candidate.IsShortListed)
            {
                return GenerateRanking();
            }

            if (!stage.IsShortlisted() && candidate.IsShortListed)
            {
                return null;
            }

            return candidate.Ranking;
        }

        private int GenerateRanking() => _selectedCandidates.Max(x => x.Ranking ?? 0) + 1;

        private static void ValidateRankings(IEnumerable<(Guid Id, int Ranking)> rankings)
        {
            var isDuplicateExist = rankings.Count() != rankings.Select(x => x.Ranking).Distinct().Count(); 

            if (isDuplicateExist)
            {
                throw new DomainException("Duplicated rankings is not allowed",
                    ErrorCodes.Job.Candidate.DuplicatedRankNotAllowed);
            }
        }

        private JobSelectedCandidate? GetSelectedCandidate(Guid id) => 
            _selectedCandidates.FirstOrDefault(x => x.CandidateId == id);

        private JobArchivedCandidate? GetArchivedCandidate(Guid id) =>
            _archivedCandidates.FirstOrDefault(x => x.CandidateId == id);

        private JobCandidateBase? GetJobCandidate(Guid id)
        {
            var jobSelectedCandidate = GetSelectedCandidate(id);
            if (jobSelectedCandidate is not null)
            {
                return jobSelectedCandidate;
            }
            var jobArchivedCandidate = GetArchivedCandidate(id);
            if (jobArchivedCandidate is not null)
            {
                return jobArchivedCandidate;
            }
            return null;
        }

        private void Shorlist()
        {
            var shortListedCandidates = _selectedCandidates
                .Where(c => c.IsShortListed)
                .ToList();

            if (!shortListedCandidates.Any())
            {
                throw new DomainException($"There is no candidates in ShortList for job id: {Id}.",
                    ErrorCodes.Job.Candidate.ShortlistedMandatory);
            }

            ShortListActivatedAt = DateTimeOffset.UtcNow;
            Stage = JobStage.ShortListed;

            AddEvent(new JobCandidatesShortlistedDomainEvent(this, ShortListActivatedAt.Value));
        }
    }
}
