using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using Jobs.Domain.Aggregates.JobAggregate;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Jobs.Domain.Aggregates.JobAggregate.ValueObjects;
using Jobs.Domain.Events.JobAggregate;
using Jobs.Domain.UnitTests.JobAggregate.DataSeed;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Jobs.Domain.UnitTests.JobAggregate
{
    public class JobTests
    {
        [Theory, AutoMoqData]
        public void Initialize_ShouldInitializeJob(
            Position position,
            ICollection<WorkType> workTypes,
            bool isUrgent)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();
            var job = new Job();
            var expectedStage = JobStage.Pending;

            // Act
            job.Initialize(
                GetValidCompany(job.Id),
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                workTypes,
                isUrgent);

            // Assert
            Assert.Equal(expectedStage, job.Stage);
            Assert.Equal(isUrgent, job.Terms?.IsUrgent);
            AssertJobInitializationData(
                null,
                position,
                job.YearExperience,
                null,
                null,
                workTypes,
                job);
        }

        [Theory, AutoMoqData]
        public void Create_ShouldCreateJob(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();
            var job = new Job();

            // Act
            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Assert
            AssertJobInitializationData(
                owner,
                position,
                yearExperience,
                jobDataSeed.DeadlineDate,
                description,
                workTypes,
                job);
        }

        [Theory, AutoMoqData]
        public void Create_ShouldThrowDomainException_WhenWorkTypesIsEmpty(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var job = new Job();
            var jobDataSeed = new JobDataSeed();
            var workTypes = new List<WorkType>();

            // Act
            var action = () => job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(0)]
        [InlineAutoMoqData(1)]
        [InlineAutoMoqData(5)]
        public void Create_ShouldThrowDomainException_WhenStartDateInvalid(
            int minusDays,
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var job = new Job();
            var jobDataSeed = new JobDataSeed();
            jobDataSeed.SetInvalidStartDate(minusDays);

            var workTypes = new List<WorkType>();

            // Act
            var action = () => job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(0)]
        [InlineAutoMoqData(1)]
        [InlineAutoMoqData(5)]
        public void Create_ShouldThrowDomainException_WhenDeadlineDateInvalid(
            int minusDays,
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var job = new Job();
            var jobDataSeed = new JobDataSeed();
            jobDataSeed.SetInvalidDeadlineDate(minusDays);

            var workTypes = new List<WorkType>();

            // Act
            var action = () => job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateJob(
            Job job,
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            ICollection<JobAssignedEmployee> assignedEmployees,
            ICollection<JobSkill> skills,
            ICollection<JobIndustry> industries,
            ICollection<JobSeniority> seniorities,
            ICollection<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();

            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            var expectedSkillCodes = skills.Select(s => s.Code).ToHashSet();
            var expectedIndustriesCodes = industries.Select(s => s.Code).ToHashSet();
            var expectedAssignedEmployeesIds = assignedEmployees.Select(x => x.Employee.Id).ToHashSet();
            var expectedSeniorities = seniorities.Select(x => x.Seniority).ToHashSet();
            var expectedLanguageIds = languages.Select(x => x.Language.Id).ToHashSet();
            var expectedInterestedCandidatesIds = interestedCandidates.Select(x => x.CandidateId).ToHashSet();
            var expectedInterestedLinkedIns = interestedLinkedIns.ToHashSet();

            // Act
            job.Update(
                job.Owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Assert
            AssertJobInitializationData(
                owner,
                position,
                yearExperience,
                jobDataSeed.DeadlineDate,
                description,
                workTypes,
                job);

            Assert.Equal(expectedSkillCodes, job.Skills.Select(x => x.Code));
            Assert.Equal(expectedIndustriesCodes, job.Industries.Select(x => x.Code));
            Assert.Equal(expectedAssignedEmployeesIds, job.AssignedEmployees.Select(x => x.Employee.Id));
            Assert.Equal(expectedSeniorities, job.SeniorityLevels.Select(x => x.Seniority));
            Assert.Equal(expectedLanguageIds, job.Languages.Select(x => x.Language.Id));
            Assert.Equal(expectedInterestedCandidatesIds, job.InterestedCandidates.Select(x => x.CandidateId));
            Assert.Equal(expectedInterestedLinkedIns, job.InterestedLinkedIns.Select(x => x.Url));
        }

        [Theory, AutoMoqData]
        public void ShareViaEmail_ShouldInitSharingWithNewKey_WhenSharingNotExist(Job job, string email)
        {
            // Act
            job.ShareViaEmail(email);

            // Assert
            Assert.NotEqual(Guid.Empty, job.Sharing!.Key);
        }

        [Theory, AutoMoqData]
        public void ShareViaEmail_ShouldInitSharingWithSameKeyAndDifferentDate_WhenSharingExist(Job job, string email)
        {
            // Arrange
            job.ShareViaEmail(email);
            var expectedRouteKey = job.Sharing!.Key;
            var existingDate = job.Sharing!.Date;

            // Act
            job.ShareViaEmail(email);

            // Assert
            Assert.Equal(expectedRouteKey, job.Sharing.Key);
            Assert.NotEqual(existingDate, job.Sharing!.Date);
        }

        [Theory, AutoMoqData]
        public void ShareViaLink_ShouldInitSharingWithNewKey_WhenSharingNotExist(Job job)
        {
            // Act
            job.ShareViaLink();

            // Assert
            Assert.NotEqual(Guid.Empty, job.Sharing!.Key);
        }

        [Theory, AutoMoqData]
        public void ShareViaLink_ShouldInitSharingWithSameKeyAndDifferentDate_WhenSharingExist(Job job)
        {
            // Arrange
            job.ShareViaLink();
            var expectedRouteKey = job.Sharing!.Key;
            var existingDate = job.Sharing!.Date;

            // Act
            job.ShareViaLink();

            // Assert
            Assert.Equal(expectedRouteKey, job.Sharing.Key);
            Assert.NotEqual(existingDate, job.Sharing!.Date);
        }

        [Theory, AutoMoqData]
        public void StartSearchAndSelect_ShouldChangeJobStageToCandidateSelection_WhenStageIsCalibration(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();
            var expectedJobStage = JobStage.CandidateSelection;

            var job = new Job();
            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Act
            job.StartSearchAndSelection();

            // Assert
            Assert.Equal(expectedJobStage, job.Stage);
            Assert.True(job.IsSelectionStarted);
        }

        [Theory, AutoMoqData]
        public void Archive_ShouldKeepIsSelectionStarted(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();

            var job = new Job();
            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            job.StartSearchAndSelection();
            var stage = JobStage.OnHold;

            // Act
            job.Archive(stage);

            // Assert
            Assert.Equal(stage, job.Stage);
            Assert.True(job.IsArchived);
            Assert.True(job.IsSelectionStarted);
        }

        [Theory, AutoMoqData]
        public void StartSearchAndSelect_ShouldThrowDomainException_WhenStageIsCandidateSelection(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();
            var job = new Job();

            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);
            job.StartSearchAndSelection();

            // Act
            var action = () => job.StartSearchAndSelection();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Publish_ShouldSetIsPublishedAsTrue(
            Employee owner,
            Position position,
            string? description,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalaryFrom,
            decimal? freelanceHourlySalaryTo,
            decimal? freelanceMonthlySalaryFrom,
            decimal? freelanceMonthlySalaryTo,
            decimal? permanentMonthlySalaryFrom,
            decimal? permanentMonthlySalaryTo,
            YearExperience yearExperience,
            int? hoursPerProject,
            bool isPriority,
            bool isUrgent,
            ICollection<WorkingHoursType> workingHourTypes,
            ICollection<WorkType> workTypes,
            IEnumerable<JobAssignedEmployee> assignedEmployees,
            IEnumerable<JobSkill> skills,
            IEnumerable<JobIndustry> industries,
            IEnumerable<JobSeniority> seniorities,
            IEnumerable<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns)
        {
            // Arrange
            var jobDataSeed = new JobDataSeed();

            var job = new Job();
            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                jobDataSeed.DeadlineDate,
                description,
                jobDataSeed.StartDate,
                jobDataSeed.EndDate,
                weeklyWorkHours,
                currency,
                hoursPerProject,
                freelanceHourlySalaryFrom,
                freelanceHourlySalaryTo,
                freelanceMonthlySalaryFrom,
                freelanceMonthlySalaryTo,
                permanentMonthlySalaryFrom,
                permanentMonthlySalaryTo,
                yearExperience.From,
                yearExperience.To,
                isPriority,
                isUrgent,
                workingHourTypes,
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Act
            job.Publish();
            
            // Assert
            Assert.True(job.IsPublished);

            var jobPublishedEvent = job.DomainEvents.Last() as JobPublishedDomainEvent;
            Assert.NotNull(jobPublishedEvent);
        }

        [Theory, AutoMoqData]
        public void UpdateJobOwner_ShouldUpdate(
            Employee owner,
            Position position,
            ICollection<WorkType> workTypes,
            ICollection<JobSkill> skills,
            ICollection<JobIndustry> industries,
            ICollection<JobAssignedEmployee> assignedEmployees,
            ICollection<JobSeniority> seniorities,
            ICollection<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns,
            string newOwnerFirstName,
            string newOwnerLastName,
            string? newOwnerPictureUri,
            bool isPriority,
            bool isUrgent)
        {
            // Arrange
            var job = new Job();

            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                isPriority,
                isUrgent,
                new List<WorkingHoursType>(),
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Act
            job.UpdateJobOwner(newOwnerFirstName, newOwnerLastName, newOwnerPictureUri);

            // Assert
            Assert.NotSame(owner, job.Owner);
            Assert.Equal(newOwnerFirstName, job.Owner!.FirstName);
            Assert.Equal(newOwnerLastName, job.Owner!.LastName);
            Assert.Equal(newOwnerPictureUri, job.Owner!.PictureUri);
        }

        [Theory, AutoMoqData]
        public void UpdateAssignedEmployee_ShouldUpdate(
            Employee owner,
            Position position,
            ICollection<WorkType> workTypes,
            ICollection<JobSkill> skills,
            ICollection<JobIndustry> industries,
            ICollection<JobAssignedEmployee> assignedEmployees,
            ICollection<JobSeniority> seniorities,
            ICollection<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns,
            string employeeFirstName,
            string employeeLastName,
            string? employeePictureUri,
            bool isPriority,
            bool isUrgent)
        {
            // Arrange
            var job = new Job();
            var employeeId = assignedEmployees.FirstOrDefault()!.Employee.Id;

            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                isPriority,
                isUrgent,
                new List<WorkingHoursType>(),
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Act
            job.UpdateAssignedEmployee(employeeId, employeeFirstName, employeeLastName, employeePictureUri);

            // Assert
            var updatedEmployee = Assert.Single(job.AssignedEmployees.Where(x => x.Employee.Id == employeeId));

            Assert.Equal(employeeFirstName, updatedEmployee.Employee.FirstName);
            Assert.Equal(employeeLastName, updatedEmployee.Employee.LastName);
            Assert.Equal(employeePictureUri, updatedEmployee.Employee.PictureUri);
        }

        [Theory, AutoMoqData]
        public void UpdateCompanyAddress_ShouldSetJobLocation(
            Guid companyId,
            CompanyStatus status,
            string name,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            string? description,
            string? logoUri,
            JobContactPerson contactPersons)
        {
            // Arrange
            var job = new Job();
            var company = new Company(
                companyId,
                status,
                name, 
                addressLine, 
                city,
                country, 
                postalCode,
                longitude,
                latitude,
                description,
                logoUri, 
                new List<JobContactPerson> { contactPersons });

            var expectedLocation = $"{company.Address?.City} ({company.Address?.Country})";

            // Act
            job.UpdateCompany(company);

            // Assert
            Assert.Equal(expectedLocation, job.Location);
        }

        [Theory, AutoMoqData]
        public void SyncSkill_ShouldPushSkillSyncedDomainEvent(Guid skillId, Guid? skillAliasId, string? skillAliasCode)
        {
            // Arrange
            var job = new Job();

            // Act
            job.SyncSkill(skillId, skillAliasId, skillAliasCode);

            // Assert
            var @event = Assert.Single(job.DomainEvents);
            var jobSkillSyncedEvent = @event as JobSkillSyncedDomainEvent;
            Assert.NotNull(jobSkillSyncedEvent);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSyncJobPositionAndPushDomainEvent(
            Guid? positionAliasId, 
            string? positionAliasCode,
            Employee owner,
            Position position,
            ICollection<WorkType> workTypes,
            ICollection<JobSkill> skills,
            ICollection<JobIndustry> industries,
            ICollection<JobAssignedEmployee> assignedEmployees,
            ICollection<JobSeniority> seniorities,
            ICollection<JobLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<JobInterestedCandidate> interestedCandidates,
            IEnumerable<string> interestedLinkedIns,
            bool isPriority,
            bool isUrgent)
        {
            // Arrange
            var job = new Job();
            job.Create(
                GetValidCompany(job.Id),
                owner,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                isPriority,
                isUrgent,
                new List<WorkingHoursType>(),
                workTypes,
                assignedEmployees,
                skills,
                industries,
                seniorities,
                languages,
                formats,
                interestedCandidates,
                interestedLinkedIns);

            // Act
            job.SyncJobPositions(position.Id, positionAliasId, positionAliasCode);

            // Assert
            Assert.Equal(position.Id, job.Position.Id);
            Assert.Equal(position.Code, job.Position.Code);
            Assert.Equal(positionAliasId, job.Position.AliasTo?.Id);
            Assert.Equal(positionAliasCode, job.Position.AliasTo?.Code);

            var jobPositionSyncedEvent = job.DomainEvents.Last() as JobPositionSyncedDomainEvent;
            Assert.NotNull(jobPositionSyncedEvent);
        }

        private static void AssertJobInitializationData(
            Employee? owner,
            Position position,
            YearExperience? yearExperience,
            DateTimeOffset? deadLineDate,
            string? description,
            ICollection<WorkType> workTypes,
            Job job)
        {
            Assert.NotEqual(Guid.Empty, job.Id);
            Assert.Equal(owner, job.Owner);
            Assert.Equal(position.Id, job.Position.Id);
            Assert.Equal(position.Code, job.Position.Code);
            Assert.Equal(position.AliasTo?.Id, job.Position.AliasTo?.Id);
            Assert.Equal(position.AliasTo?.Code, job.Position.AliasTo?.Code);
            Assert.Equal(deadLineDate, job.DeadlineDate);
            Assert.Equal(description, job.Description);
            Assert.Equal(yearExperience?.From, job.YearExperience?.From);
            Assert.Equal(yearExperience?.To, job.YearExperience?.To);

            if (workTypes.Any(x => x == WorkType.Freelance))
            {
                Assert.NotNull(job.Terms?.Freelance);
            }
            else
            {
                Assert.Null(job.Terms?.Freelance);
            }

            if (workTypes.Any(x => x == WorkType.Permanent))
            {
                Assert.NotNull(job.Terms?.Permanent);
            }
            else
            {
                Assert.Null(job.Terms?.Permanent);
            }
        }

        private static Company GetValidCompany(Guid jobId)
        {
            var companyId = Guid.NewGuid();
            var status = CompanyStatus.Approved;
            var name = "CompanyName";
            var personId = Guid.NewGuid();
            var firstName = "Firstname";
            var lastName = "Lastname";
            var email = "email@email.com";

            var contactPersons = new List<JobContactPerson>
            {
                new JobContactPerson(jobId, personId, true, firstName, lastName, null, email, null, null, null, null, null, null, null),
                new JobContactPerson(jobId, personId, false, firstName, lastName, null, email, null, null, null, null, null, null, null)
            };

            return new Company(
                companyId,
                status,
                name,
                "Lithuania, Vilnius, Zalgirio g. 135",
                "Vilnius",
                "Lithuania",
                "08217",
                54.704879,
                25.271569,
                null,
                null,
                contactPersons);
        }
    }
}
