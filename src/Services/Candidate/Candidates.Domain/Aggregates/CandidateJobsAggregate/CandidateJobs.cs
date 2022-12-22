using Candidates.Domain.Aggregates.CandidateJobsAggregate.Entities;
using Candidates.Domain.Events.CandidateJobsAggregate;
using Domain.Seedwork;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Extensions;

namespace Candidates.Domain.Aggregates.CandidateJobsAggregate
{
    public class CandidateJobs : AggregateRoot
    {
        public bool IsShortlisted { get; private set; }
        public bool IsHired { get; private set; }

        private readonly List<CandidateSelectedInJob> _selectedInJobs = new();
        public IReadOnlyList<CandidateSelectedInJob> SelectedInJobs => _selectedInJobs;

        private readonly List<CandidateAppliedInJob> _appliedInJobs = new();
        public IReadOnlyList<CandidateAppliedInJob> AppliedInJobs => _appliedInJobs;

        private readonly List<CandidateArchivedInJob> _archivedInJobs = new();
        public IReadOnlyList<CandidateArchivedInJob> ArchivedInJobs => _archivedInJobs;

        private CandidateJobs() { }

        public CandidateJobs(Guid candidateId)
        {
            Id = candidateId;
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void SyncNewlyAddedSelectedJob(CandidateSelectedInJob job)
        {
            if (job.Stage != SelectedCandidateStage.New)
            {
                throw new DomainException($"Only job with status {SelectedCandidateStage.New} can be synced",
                    ErrorCodes.Candidate.Jobs.JobForbbidenToSync);
            }

            if (_archivedInJobs.Any(x => x.JobId == job.JobId))
            {
                throw new DomainException("Job is already archived and cannot be synced",
                    ErrorCodes.Candidate.Jobs.JobAlreadyArchived);
            }

            if (_selectedInJobs.Any(x => x.JobId == job.JobId))
            {
                throw new DomainException("Job is already added and cannot be synced",
                    ErrorCodes.Candidate.Jobs.JobAlreadyAdded);
            }

            _selectedInJobs.Add(job);

            AddEvent(new CandidateJobsAddedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncCandidateSelectedJob(CandidateSelectedInJob job)
        {
            var archivedJob = GetArchivedInJob(job.JobId);

            if (archivedJob is not null)
            {
                job.SyncCandidateJobAttachments(archivedJob.MotivationVideo, archivedJob.CoverLetter);
                _archivedInJobs.Remove(archivedJob);
            }

            var selectedJob = GetSelectedInJob(job.JobId);

            if (selectedJob is null)
            {
                _selectedInJobs.Add(job);
            }
            else
            {
                selectedJob.Sync(job.Stage, job.InvitedAt, job.HasApplied);
            }

            var newIsShortlistedStatus = CalculateIsShortlisted();
            
            if (IsShortlisted != newIsShortlistedStatus)
            {
                IsShortlisted = newIsShortlistedStatus;
                AddShortListedChangedEvent();
            }

            AddEvent(new CandidateJobsUpdatedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncCandidateArchivedJob(CandidateArchivedInJob job)
        {
            var selectedJob = GetSelectedInJob(job.JobId);

            if (selectedJob is not null)
            {
                job.SyncCandidateJobAttachments(selectedJob.MotivationVideo, selectedJob.CoverLetter);
                _selectedInJobs.Remove(selectedJob);
            }

            var archivedJob = GetArchivedInJob(job.JobId);

            if (archivedJob is null)
            {
                _archivedInJobs.Add(job);
            }
            else
            {
                archivedJob.Sync(job.Stage, job.InvitedAt, job.HasApplied);
            }

            var newIsShortlistedStatus = CalculateIsShortlisted();

            if (IsShortlisted != newIsShortlistedStatus)
            {
                IsShortlisted = newIsShortlistedStatus;
                AddEvent(new CandidateJobsUnshortlistedDomainEvent(this, DateTimeOffset.UtcNow));
            }
            
            AddEvent(new CandidateJobsArchivedDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncSelectedCandidateHiredJob(CandidateSelectedInJob job)
        {
            var selectedJob = GetSelectedInJob(job.JobId);

            if (selectedJob is null)
            {
                _selectedInJobs.Add(job);
            }
            else
            {
                selectedJob.Sync(job.Stage, job.InvitedAt, job.HasApplied);
                selectedJob.ChangeJobStage(job.JobStage);
            }

            IsHired = CalculateIsHired();

            if (IsShortlisted)
            {
                var newIsShortlistedStatus = CalculateIsShortlisted();
                if (IsShortlisted != newIsShortlistedStatus)
                {
                    IsShortlisted = false;
                    AddEvent(new CandidateJobsUnshortlistedDomainEvent(this, DateTimeOffset.UtcNow));
                }
            }

            AddEvent(new CandidateJobsHiredDomainEvent(this, DateTimeOffset.UtcNow));
        }

        public void SyncArchivedCandidateHiredJob(CandidateArchivedInJob job)
        {
            var archivedJob = GetArchivedInJob(job.JobId);

            if (archivedJob is null)
            {
                _archivedInJobs.Add(job);
            }
            else
            {
                archivedJob.ChangeJobStage(job.JobStage);
            }
        }

        public void SyncJobStage(Guid jobId, JobStage jobStage)
        {
            var appliedInJob = GetAppliedInJob(jobId);
            if (appliedInJob is not null)
            {
                appliedInJob.ChangeJobStage(jobStage);
            }

            JobInformationBase? candidateInJob = GetCandidateInJob(jobId);

            if (candidateInJob is not null)
            {
                candidateInJob.ChangeJobStage(jobStage);

                if (candidateInJob.IsJobArchived)
                {
                    AddEvent(new CandidateInJobJobArchivedDomainEvent(this, jobId, DateTimeOffset.UtcNow));
                }
            }
        }

        public void RejectJob(Guid jobId)
        {
            var candidateSelectedInJob = GetSelectedInJob(jobId);
            if (candidateSelectedInJob is null)
            {
                return;
            }
            var archivedInJob = new CandidateArchivedInJob(
                candidateSelectedInJob.JobId,
                candidateSelectedInJob.JobStage,
                candidateSelectedInJob.Position.Id,
                candidateSelectedInJob.Position.Code,
                candidateSelectedInJob.Position.AliasTo?.Id,
                candidateSelectedInJob.Position.AliasTo?.Code,
                candidateSelectedInJob.CandidateId,
                candidateSelectedInJob.Company.Id,
                candidateSelectedInJob.Company.Name,
                candidateSelectedInJob.Company.LogoUri,
                candidateSelectedInJob.Freelance,
                candidateSelectedInJob.Permanent,
                candidateSelectedInJob.StartDate,
                candidateSelectedInJob.DeadlineDate,
                ArchivedCandidateStage.NotInterested,
                candidateSelectedInJob.InvitedAt,
                candidateSelectedInJob.HasApplied);

            _selectedInJobs.Remove(candidateSelectedInJob);
            _archivedInJobs.Add(archivedInJob);

            var newIsShortlistedStatus = CalculateIsShortlisted();

            if (IsShortlisted != newIsShortlistedStatus)
            {
                IsShortlisted = newIsShortlistedStatus;
                AddEvent(new CandidateJobsUnshortlistedDomainEvent(this, DateTimeOffset.UtcNow));
            }

            AddEvent(new CandidateJobRejectedDomainEvent(this, jobId, DateTimeOffset.UtcNow));
        }

        public void ApplyToJob(
            Guid jobId,
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
            var candidateArchivedInJob = GetArchivedInJob(jobId);

            if (candidateArchivedInJob is not null)
            {
                throw new DomainException(
                    "Apply is not allowed for archived job",
                    ErrorCodes.Candidate.Jobs.ApplyNotAllowedForArchivedJob);
            }
            if (GetAppliedInJob(jobId) is not null)
            {
                throw new DomainException("Candidate already applied for this job", ErrorCodes.Candidate.Jobs.AlreadyApplied);
            }

            var appliedCandidate = new CandidateAppliedInJob(
                jobId,
                jobStage,
                positionId,
                positionCode,
                positionAliasToId,
                positionAliasToCode,
                Id,
                companyId,
                companyName,
                companyLogo,
                freelance,
                permanent,
                startDate,
                deadlineDate,
                null);

            _appliedInJobs.Add(appliedCandidate);

            AddEvent(new CandidateAppliedToJobDomainEvent(this, jobId, DateTimeOffset.UtcNow));
        }

        public void SyncJobInformation(
            Guid jobId,
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
            JobInformationBase? candidateInJob = GetCandidateInJob(jobId);

            if (candidateInJob is not null)
            {
                candidateInJob.SyncJobInformation(
                    jobStage,
                    positionId,
                    positionCode,
                    positionAliasToId,
                    positionAliasToCode,
                    companyId,
                    companyName,
                    companyLogo,
                    freelance,
                    permanent,
                    startDate,
                    deadlineDate);
            }

            var candidateAppliedInJob = GetAppliedInJob(jobId);
            if (candidateAppliedInJob is not null)
            {
                candidateAppliedInJob.SyncJobInformation(
                    jobStage,
                    positionId,
                    positionCode,
                    positionAliasToId,
                    positionAliasToCode,
                    companyId,
                    companyName,
                    companyLogo,
                    freelance,
                    permanent,
                    startDate,
                    deadlineDate);
            }
        }

        public void UpdateCandidateSelectedInJob(
            Guid jobId, 
            string? motivationVideoUri,
            string? motivationVideoFileName,
            bool isMotivationVideoChanged,
            Guid? motivationVideoCacheId,
            string? coverLetter)
        {
            var selectedInJob = GetSelectedInJob(jobId);

            if (selectedInJob is null)
            {
                throw new NotFoundException("Selected candidate in job not found", 
                    ErrorCodes.Candidate.Jobs.SelectedInJobNotFound);
            }

            selectedInJob.Update(
                motivationVideoUri,
                motivationVideoFileName,
                isMotivationVideoChanged,
                coverLetter);

            if (isMotivationVideoChanged && motivationVideoCacheId.HasValue)
            {
                AddEvent(new CandidateJobsFilesAddedDomainEvent(
                    new List<Guid>() { motivationVideoCacheId.Value },
                    DateTimeOffset.UtcNow));
            }
        }

        public CandidateInJobBase? GetCandidateInJob(Guid id)
        {
            var selectedInJob = GetSelectedInJob(id);
            if (selectedInJob is not null)
            {
                return selectedInJob;
            }
            var archivedInJob = GetArchivedInJob(id);
            if (archivedInJob is not null)
            {
                return archivedInJob;
            }
            return null;
        }

        public void RemoveCandidateJobMotivationVideo(Guid jobId)
        {
            var candidteInJob = GetCandidateInJob(jobId);
            if (candidteInJob is not null)
            {
                candidteInJob.RemoveMotivationVideo();
            }
        }

        public void SyncJobPositions(Guid id, Guid? aliasId, string? aliasCode)
        {
            var appliedInJobs = _appliedInJobs.Where(x => x.Position.Id == id);
            var selectedInJobs = _selectedInJobs.Where(x => x.Position.Id == id);
            var archivedInJobs = _archivedInJobs.Where(x => x.Position.Id == id);

            var candidateInJobs = new List<JobInformationBase>()
                .Concat(appliedInJobs)
                .Concat(selectedInJobs)
                .Concat(archivedInJobs);

            foreach (var candidateInJob in candidateInJobs)
            {
                candidateInJob.SyncJobPosition(aliasId, aliasCode);
            }
        }

        private bool CalculateIsShortlisted() => _selectedInJobs
            .Where(x => !x.JobStage.IsArchived())
            .Any(x => x.Stage.IsShortlisted());

        private bool CalculateIsHired() => _selectedInJobs
            .Any(x => x.Stage == SelectedCandidateStage.Hired);

        private void AddShortListedChangedEvent()
        {
            if (IsShortlisted)
            {
                AddEvent(new CandidateJobsShortlistedDomainEvent(this, DateTimeOffset.UtcNow));
            }
            else
            {
                AddEvent(new CandidateJobsUnshortlistedDomainEvent(this, DateTimeOffset.UtcNow));
            }
        }

        private CandidateSelectedInJob? GetSelectedInJob(Guid id) =>
            _selectedInJobs.FirstOrDefault(x => x.JobId == id);

        private CandidateArchivedInJob? GetArchivedInJob(Guid id) =>
            _archivedInJobs.FirstOrDefault(x => x.JobId == id);

        private CandidateAppliedInJob? GetAppliedInJob(Guid id) =>
            _appliedInJobs.FirstOrDefault(x => x.JobId == id);
    }
}
