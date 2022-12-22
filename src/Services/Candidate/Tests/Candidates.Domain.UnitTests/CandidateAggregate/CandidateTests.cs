using Candidates.Domain.Aggregates.CandidateAggregate;
using Candidates.Domain.Aggregates.CandidateAggregate.Entities;
using Candidates.Domain.Events.CandidateAggregate;
using Candidates.Domain.UnitTests.CandidateAggregate.DataSeed;
using Domain.Seedwork.Enums;
using Domain.Seedwork.Exceptions;
using Domain.Seedwork.Shared.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Shared;
using Xunit;

namespace Candidates.Domain.UnitTests.CandidateAggregate
{
    public class CandidateTests
    {
        [Fact]
        public void Create_ShouldCreateCandidateWithIdAndCreateAtDate()
        {
            // Arrange Act
            var candidate = new Candidate();

            // Assert
            Assert.NotEqual(Guid.Empty, candidate.Id);
            Assert.True(candidate.CreatedAt < DateTimeOffset.UtcNow);
        }

        [Theory, AutoMoqData]
        public void RegisterMyself_ShouldThrowException_WhenAlreadyExists(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            Guid otherExternalId)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            Action action = () => candidate.RegisterMyself(
                candidateTestData.Email!,
                otherExternalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Throws<DomainException>(action);
        }


        [Theory, AutoMoqData]
        public void RegisterMyself_ShouldRegisterCandidateWithEmailAndExternalId(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            var expectedIsVerified = false;
            var expectedStatus = CandidateStatus.Registered;

            // Act
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Equal(externalId, candidate.ExternalId);
            Assert.Equal(candidateTestData.Email, candidate.Email!.Address);
            Assert.Equal(expectedIsVerified, candidate.Email!.IsVerified);
            Assert.NotEqual(Guid.Empty, candidate.Email!.VerificationKey);
            Assert.NotEqual(DateTimeOffset.MinValue, candidate.Email!.VerificationRequestedAt);
            Assert.Null(candidate.Email.VerifiedAt);
            Assert.Equal(expectedStatus, candidate.Status);
            Assert.Equal(systemLanguage, candidate.SystemLanguage);
            Assert.Equal(acceptTermsAndConditions, candidate.TermsAndConditions!.Agreed);
            Assert.Equal(acceptMarketingAcknowledgement, candidate.MarketingAcknowledgement!.Agreed);
        }

        [Theory, AutoMoqData]
        public void LinkCandidate_ShouldLinkCandidate(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();

            var candidate = new Candidate();
            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            var expectedIsVerified = false;
            var expectedStatus = CandidateStatus.Approved;

            // Act
            candidate.LinkCandidate(
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Equal(CandidateStatus.Approved, candidate.Status);
            Assert.Equal(externalId, candidate.ExternalId);
            Assert.Equal(expectedIsVerified, candidate.Email?.IsVerified);
            Assert.NotEqual(Guid.Empty, candidate.Email!.VerificationKey);
            Assert.NotEqual(DateTimeOffset.MinValue, candidate.Email!.VerificationRequestedAt);
            Assert.Null(candidate.Email.VerifiedAt);
            Assert.Equal(expectedStatus, candidate.Status);
            Assert.Equal(systemLanguage, candidate.SystemLanguage);
            Assert.Equal(acceptTermsAndConditions, candidate.TermsAndConditions!.Agreed);
            Assert.Equal(acceptMarketingAcknowledgement, candidate.MarketingAcknowledgement!.Agreed);
        }

        [Theory, AutoMoqData]
        public void LinkCandidate_ShouldThrowException_WhenAlreadyExists(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            Guid otherExternalId,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();

            var candidate = new Candidate();
            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            candidate.LinkCandidate(
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            Action action = () => candidate.LinkCandidate(
                otherExternalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void LinkCandidate_ShouldThrowException_WhenEmailNotExists(
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            Guid otherExternalId,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();

            var candidate = new Candidate();
            candidate.Initialize(
                null,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Act
            var action = () => candidate.LinkCandidate(
                otherExternalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldInitializeCandidateWithEmailAndExternalId(
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            // Act
            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            AssertCandidate(
                candidate,
                candidateTestData.Email!,
                firstName,
                lastName,
                currentPosition,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                true,
                videoUri,
                videoFileName,
                true,
                industries,
                desiredSkills,
                skills,
                languages,
                hobbies,
                formats,
                workTypes);

            Assert.Equal(educations.Count(), candidate.Educations.Count);
            Assert.Equal(courses.Count(), candidate.Courses.Count);
            Assert.Equal(workExperiences.Count(), candidate.WorkExperiences.Count);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldUpdateCandidate(
            string? firstName,
            string? lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,   
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            bool isCurriculumVitaeChanged,
            string? videoUri,
            string? videoFileName,
            bool isVideoChanged,
            Dictionary<ImageType, string?>? picturePaths,
            bool isPictureChanged,
            IEnumerable<ActivityStatus> activityStatuses,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            // Act
            candidate.Update(
                candidateTestData.Email,
                firstName,
                lastName,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                isCurriculumVitaeChanged,
                videoUri,
                videoFileName,
                isVideoChanged,
                picturePaths,
                isPictureChanged,
                activityStatuses,
                industries,
                skills,
                desiredSkills,
                languages,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            AssertCandidate(
                candidate,
                candidateTestData.Email!,
                firstName,
                lastName,
                currentPosition,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                isCurriculumVitaeChanged,
                videoUri,
                videoFileName,
                isVideoChanged,
                industries,
                desiredSkills,
                skills,
                languages,
                hobbies,
                formats,
                workTypes);
        }

        [Theory, AutoMoqData]
        public void Update_ShouldThrowException_WhenNotAllowedToModify(
            string? firstName,
            string? lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            bool isCurriculumVitaeChanged,
            string? videoUri,
            string? videoFileName,
            bool isVideoChanged,
            Dictionary<ImageType, string?>? picturePaths,
            bool isPictureChanged,
            IEnumerable<ActivityStatus> activityStatuses,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            // Act
            var action = () => candidate.Update(
                candidateTestData.Email,
                firstName,
                lastName,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                isCurriculumVitaeChanged,
                videoUri,
                videoFileName,
                isVideoChanged,
                picturePaths,
                isPictureChanged,
                activityStatuses,
                industries,
                skills,
                desiredSkills,
                languages,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void AddWorkExperience_ShouldAddNewWorkExperienceToList(
            string companyName,
            Guid positionId,
            string positionCode,
            Guid? positionAliasId,
            string? positionAliasCode,
            DateTimeOffset from,
            string? jobDescription,
            bool isCurrentJob,
            WorkExperienceType type,
            IEnumerable<CandidateWorkExperienceSkill> skills)
        {
            // Arrange
            var candidate = new Candidate();
            var expectedWorkExperienceCount = candidate.WorkExperiences.Count + 1;

            // Act
            candidate.AddWorkExperience(
                type,
                companyName,
                positionId,
                positionCode,
                positionAliasId,
                positionAliasCode,
                from,
                null,
                jobDescription,
                isCurrentJob,
                skills);

            // Assert
            Assert.Equal(expectedWorkExperienceCount, candidate.WorkExperiences.Count);
        }

        [Theory]
        [InlineAutoMoqData("Company name")]
        public void DeleteWorkExperience_ShouldDeleteWorkExperienceFromList(
            string companyName,
            CandidateWorkExperience workExperience,
            WorkExperienceType type,
            Position position)
        {
            // Arrange
            var date = DateTimeOffset.Parse("2022-04-27T07:03:09.506Z");
            var candidate = new Candidate();

            candidate.AddWorkExperience(
                type,
                companyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                date,
                null,
                workExperience.JobDescription,
                workExperience.IsCurrentJob,
                workExperience.Skills);

            var expectedWorkExperienceCount = candidate.WorkExperiences.Count - 1;
            var workExperienceId = candidate.WorkExperiences.First().Id;

            // Act
            candidate.DeleteWorkExperience(workExperienceId);

            // Assert
            Assert.Equal(expectedWorkExperienceCount, candidate.WorkExperiences.Count);
        }

        [Theory, AutoMoqData]
        public void UpdateWorkExperience_ShouldThrowException_WhenWorkExperienceNotExist(
            CandidateWorkExperience workExperience, 
            Position position,
            DateTimeOffset from,
            DateTimeOffset? to)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            var action = () => candidate.UpdateWorkExperience(
                Guid.NewGuid(),
                workExperience.Type,
                workExperience.CompanyName,
                position.Id,
                position.Code,
                position.AliasTo?.Id,
                position.AliasTo?.Code,
                from,
                to,
                workExperience.JobDescription,
                workExperience.IsCurrentJob,
                workExperience.Skills);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void AddCourse_ShouldAddNewCourseToList(
            string name,
            string issuingOrganization,
            string? description,
            string certificateUri,
            string? certificateFileName,
            Guid? certificateCacheId)
        {
            // Arrange
            var candidate = new Candidate();
            var expectedCourseCount = candidate.Courses.Count + 1;

            // Act
            candidate.AddCourse(
                name, 
                issuingOrganization, 
                description,
                certificateUri,
                certificateFileName,
                certificateCacheId);

            // Assert
            Assert.Equal(expectedCourseCount, candidate.Courses.Count);
        }

        [Theory]
        [InlineAutoMoqData("name", "IssuingOrganization")]
        public void DeleteCourse_ShouldDeleteCourseFromList(
            string name,
            string issuingOrganization,
            CandidateCourse course,
            Guid? certificateCacheId)
        {
            // Arrange
            var candidate = new Candidate();
            candidate.AddCourse(
                name,
                issuingOrganization,
                course.Description,
                course.Certificate?.Uri,
                course.Certificate?.FileName,
                certificateCacheId);

            var expectedCourseCount = candidate.Courses.Count - 1;
            var courseId = candidate.Courses.First().Id;

            // Act
            candidate.DeleteCourse(courseId);

            // Assert
            Assert.Equal(expectedCourseCount, candidate.Courses.Count);
        }

        [Theory, AutoMoqData]
        public void UpdateCourse_ShouldThrowException_WhenCourseNotExist(CandidateCourse course, Guid? certificateCacheId)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            var action = () => candidate.UpdateCourse(
                Guid.NewGuid(),
                course.Name,
                course.IssuingOrganization,
                course.Description,
                course.Certificate?.Uri,
                course.Certificate?.FileName,
                true,
                certificateCacheId);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void AddEducation_ShouldAddNewEducationToList(
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string? studiesDescription,
            bool isStillStudying,
            string? certificateUri,
            string? certificateFileName,
            Guid? certificateCacheId)
        {
            // Arrange
            var candidate = new Candidate();
            var expectedEducationCount = candidate.Educations.Count + 1;

            // Act
            candidate.AddEducation(
                schoolName,
                degree,
                fieldOfStudy,
                from,
                null,
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName,
                certificateCacheId);

            // Assert
            Assert.Equal(expectedEducationCount, candidate.Educations.Count);
        }

        [Theory, AutoMoqData]
        public void DeleteEducation_ShouldDeleteEducationFromList(Guid candidateId,
            string schoolName,
            string degree,
            string fieldOfStudy,
            DateTimeOffset from,
            string? studiesDescription,
            bool isStillStudying,
            string? certificateUri,
            string? certificateFileName,
            Guid? certificateCacheId)
        {
            // Arrange
            var education = new CandidateEducation(candidateId,
                schoolName,
                degree,
                fieldOfStudy,
                from,
                from.AddDays(1),
                studiesDescription,
                isStillStudying,
                certificateUri,
                certificateFileName
            );

            var candidate = new Candidate();
            candidate.AddEducation(
                education.SchoolName,
                education.Degree,
                education.FieldOfStudy,
                education.Period.From,
                education.Period.To,
                education.StudiesDescription,
                education.IsStillStudying,
                education.Certificate?.Uri,
                education.Certificate?.FileName,
                certificateCacheId);

            var expectedEducationCount = candidate.Educations.Count - 1;
            var educationId = candidate.Educations.First().Id;

            // Act
            candidate.DeleteEducation(educationId);

            // Assert
            Assert.Equal(expectedEducationCount, candidate.Educations.Count);
        }

        [Theory, AutoMoqData]
        public void UpdateEducation_ShouldThrowException_WhenEducationNotExist(CandidateEducation education, Guid? certificateCacheId)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            var action = () => candidate.UpdateEducation(
                Guid.NewGuid(),
                education.SchoolName,
                education.Degree,
                education.FieldOfStudy,
                education.Period.From,
                education.Period.To,
                education.StudiesDescription,
                education.IsStillStudying,
                education.Certificate?.Uri,
                education.Certificate?.FileName,
                true,
                certificateCacheId);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ToggleIsShortlisted_ShouldUpdate(bool isShortListed)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            candidate.ToggleIsShortlisted(isShortListed);

            // Assert
            Assert.Equal(isShortListed, candidate.IsShortListed);
        }

        [Theory]
        [InlineAutoMoqData(null, null)]
        [InlineAutoMoqData("", "")]
        [InlineAutoMoqData("  ", "  ")]
        public void Initialize_ShouldThowException_WhenNoEmailAndNoLinkedIn(
            string? email,
            string? linkedInUrl,
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? personalWebsiteUrl,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            // Act
            var action = () => candidate.Initialize(
                email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                linkedInUrl,
                personalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                null,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                null,
                null,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory]
        [InlineAutoMoqData(null, null)]
        [InlineAutoMoqData("FirstName", null)]
        [InlineAutoMoqData(null, "LastName")]
        [InlineAutoMoqData("FirstName", "  ")]
        [InlineAutoMoqData("  ", "LastName")]
        public void Initialize_ShouldThowException_WhenNoFirstNameOrLastName(
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? personalWebsiteUrl,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            // Act
            var action = () => candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                personalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                null,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                null,
                null,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldThowException_WhenNoWorkType(
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            // Act
            var action = () => candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                null,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                null,
                null,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                new List<WorkType>(),
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }


        [Theory]
        [InlineAutoMoqData(null, null)]
        [InlineAutoMoqData("", "")]
        [InlineAutoMoqData("  ", "  ")]
        public void Update_ShouldThowException_WhenNoEmailAndNoLinkedIn(
            string? emailToUpodate,
            string? linkedInUrlToUpdate,
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            IEnumerable<ActivityStatus> activityStatuses,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            Dictionary<ImageType, string?>? picturePaths,
            bool isPictureChanged,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                null,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                null,
                null,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Act
            var action = () => candidate.Update(
                emailToUpodate,
                firstName,
                lastName,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                linkedInUrlToUpdate,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                null,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                picturePaths,
                isPictureChanged,
                activityStatuses,
                industries,
                skills,
                desiredSkills,
                languages,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldVerifyCandidate(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            int verificationExpiresInMinutes = 1440;
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId, 
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = candidate.Email!.VerificationKey!.Value;

            // Act
            candidate.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.True(candidate.Email.IsVerified);
            Assert.NotNull(candidate.Email.VerifiedAt);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldThrowException_IfCandidateIsAlreadyActivated(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            int verificationExpiresInMinutes = 1440;
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = candidate.Email!.VerificationKey!.Value;
            candidate.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Act
            var action = () => candidate.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void Verify_ShouldThrowException_IfEmailVerificationExpired(
            Guid externalId,
            SystemLanguage? systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            int verificationExpiresInMinutes = 0;
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var verificationKey = candidate.Email!.VerificationKey!.Value;

            // Act
            var action = () => candidate.VerifyEmail(verificationKey, verificationExpiresInMinutes);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void RequestEmailVerification_ShouldRegenerateVerificationEmailDetails(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            candidate.RegisterMyself(
                candidateTestData.Email!, 
                externalId, 
                systemLanguage, 
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);

            var oldVerificationKey = candidate.Email!.VerificationKey!.Value;
            var oldVerificationRequestAt = candidate.Email!.VerificationRequestedAt!.Value;

            // Act
            candidate.RequestEmailVerification();

            // Assert
            Assert.NotEqual(oldVerificationKey, candidate.Email!.VerificationKey!.Value);
            Assert.True(oldVerificationRequestAt < candidate.Email!.VerificationRequestedAt!.Value);
        }

        [Fact]
        public void RequestEmailVerification_ShouldThrowException_WhenNoEmailDetails()
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            var action = () => candidate.RequestEmailVerification();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateCandidateOpenForOpportunities_ShouldUpdate(Candidate candidate, bool openForOpportunities)
        {
            // Arrange

            // Act 
            candidate.UpdateOpenForOpportunities(openForOpportunities);

            // Assert
            Assert.Equal(openForOpportunities, candidate.OpenForOpportunities);
        }

        [Theory, AutoMoqData]
        public void ApproveCandidate_ShouldApprove_WhenStatusPending(
            Guid externalId,
            SystemLanguage systemLanguage,
            bool acceptTermsAndConditions,
            bool acceptMarketingAcknowledgement,
            string firstName,
            string lastName,
            ICollection<WorkType> workTypes
            )
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();
            var activityStatuses = new List<ActivityStatus>() { ActivityStatus.Student };
            var expectedCandidateStatus = CandidateStatus.Approved;

            candidate.RegisterMyself(
                candidateTestData.Email!,
                externalId,
                systemLanguage,
                acceptTermsAndConditions,
                acceptMarketingAcknowledgement);
            candidate.VerifyEmail(candidate.Email!.VerificationKey!.Value, 100);
            candidate.Update(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                null,
                null,
                null,
                null,
                false,
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
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                false,
                null,
                null,
                false,
                null,
                false,
                activityStatuses,
                new List<CandidateIndustry>(),
                new List<CandidateSkill>(),
                new List<CandidateDesiredSkill>(),
                new List<CandidateLanguage>(),
                new List<CandidateHobby>(),
                new List<FormatType>(),
                workTypes,
                new List<Guid>());
            candidate.CompleteCoreInformation();

            // Act
            candidate.Approve();

            // Assert
            Assert.Equal(expectedCandidateStatus, candidate.Status);
        }

        [Theory, AutoMoqData]
        public void ApproveCandidate_ShouldThrowException_WhenStatusAlreadyApproved(
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            var candidate = new Candidate();

            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Act
            var action = () => candidate.Approve();

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void UpdateLegalTerms_ShouldUpdate(bool termsAgreement, bool marketingAgreement)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            candidate.UpdateLegalTerms(termsAgreement, marketingAgreement);

            // Assert
            Assert.NotNull(candidate.TermsAndConditions);
            Assert.NotNull(candidate.MarketingAcknowledgement);
            Assert.Equal(termsAgreement, candidate.TermsAndConditions!.Agreed);
            Assert.Equal(marketingAgreement, candidate.MarketingAcknowledgement!.Agreed);
        }

        [Theory, AutoMoqData]
        public void UpdateSettings_ShouldUpdate(SystemLanguage systemLanguage, bool marketingAgreement)
        {
            // Arrange
            var candidate = new Candidate();

            // Act
            candidate.UpdateSettings(systemLanguage, marketingAgreement);

            // Assert
            Assert.NotNull(candidate.MarketingAcknowledgement);
            Assert.Equal(systemLanguage, candidate.SystemLanguage);
            Assert.Equal(marketingAgreement, candidate.MarketingAcknowledgement!.Agreed);
        }

        [Theory, AutoMoqData]
        public void Initialize_ShouldThrowDomainException_WhenInvalidBirthDate(
            string firstName,
            string lastName,
            Position? currentPosition,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            ICollection<FormatType> formats,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateWorkExperience> workExperiences,
            ICollection<WorkType> workTypes,
            IEnumerable<CandidateHobby> hobbies,
            List<Guid> fileCacheIds)
        {
            // Arrange
            var candidateTestData = new CandidateDataSeed();
            candidateTestData.SetInvalidBirthdate(new Random().Next(0, 5));
            var candidate = new Candidate();
            // Act
            var action = () => candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                currentPosition?.Id,
                currentPosition?.Code,
                currentPosition?.AliasTo?.Id,
                currentPosition?.AliasTo?.Code,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                workExperiences,
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Assert
            Assert.Throws<DomainException>(action);
        }

        [Theory, AutoMoqData]
        public void SyncSkills_ShouldPushSkillsSyncedDomainEvent(Guid skillId, Guid? skillAliasId, string? skillAliasCode)
        {
            // Arrange
            var candidate = new Candidate();
            
            // Act
            candidate.SyncSkills(skillId, skillAliasId, skillAliasCode);

            // Assert
            var @event = Assert.Single(candidate.DomainEvents);
            var candidateSkillSyncedEvent = @event as CandidateSkillsSyncedDomainEvent;
            Assert.NotNull(candidateSkillSyncedEvent);
        }

        [Theory, AutoMoqData]
        public void SyncJobPosition_ShouldSyncJobPositionAndPushDomainEvent(
            Guid positionId,
            string positionCode,
            Guid newPositioAliasId,
            string newPositioAliasCode,
            string firstName,
            string lastName,
            bool openForOpportunities,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            ICollection<WorkingHoursType> workingHourTypes,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            string? videoUri,
            string? videoFileName,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateCourse> courses,
            IEnumerable<CandidateEducation> educations,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes,
            List<Guid> fileCacheIds)
        {
            // Arrange 
            var candidateTestData = new CandidateDataSeed();

            var candidate = new Candidate();
            candidate.Initialize(
                candidateTestData.Email,
                firstName,
                lastName,
                null,
                positionId,
                positionCode,
                null,
                null,
                candidateTestData.BirthDate,
                openForOpportunities,
                candidateTestData.LinkedInUrl,
                candidateTestData.PersonalWebsiteUrl,
                addressLine,
                city,
                country,
                postalCode,
                longitude,
                latitude,
                candidateTestData.StartDate,
                candidateTestData.EndDate,
                weeklyWorkHours,
                currency,
                freelanceHourlySalary,
                freelanceMonthlySalary,
                permanentMonthlySalary,
                workingHourTypes,
                candidateTestData.PhoneCountryCode,
                candidateTestData.PhoneNumber,
                yearsOfExperience,
                bio,
                curriculumVitaeUri,
                curriculumVitaeFileName,
                videoUri,
                videoFileName,
                industries,
                skills,
                desiredSkills,
                languages,
                courses,
                educations,
                new List<CandidateWorkExperience>(),
                hobbies,
                formats,
                workTypes,
                fileCacheIds);

            // Act
            candidate.SyncJobPositions(positionId, newPositioAliasId, newPositioAliasCode);

            // Assert
            Assert.Equal(positionId, candidate.CurrentPosition!.Id);
            Assert.Equal(positionCode, candidate.CurrentPosition!.Code);
            Assert.Equal(newPositioAliasId, candidate.CurrentPosition.AliasTo?.Id);
            Assert.Equal(newPositioAliasCode, candidate.CurrentPosition.AliasTo?.Code);
            
            var candidateJobPositionSyncedEvent = candidate.DomainEvents.Last() as CandidateJobPositionSyncedDomainEvent;
            Assert.NotNull(candidateJobPositionSyncedEvent);
        }

        private void AssertCandidate(
            Candidate candidate,
            string email,
            string? firstName,
            string? lastName,
            Position? currentPosition,
            DateTimeOffset? birthDate,
            bool openForOpportunities,
            string? linkedInUrl,
            string? personalWebsiteUrl,
            string? addressLine,
            string? city,
            string? country,
            string? postalCode,
            double? longitude,
            double? latitude,
            int? weeklyWorkHours,
            string? currency,
            decimal? freelanceHourlySalary,
            decimal? freelanceMonthlySalary,
            decimal? permanentMonthlySalary,
            DateTimeOffset? startDate,
            DateTimeOffset? endDate,
            ICollection<WorkingHoursType> workingHourTypes,
            string? phoneCountryCode,
            string? phoneNumber,
            int? yearsOfExperience,
            string? bio,
            string? curriculumVitaeUri,
            string? curriculumVitaeFileName,
            bool isCurriculumVitaeChanged,
            string? videoUri,
            string? videoFileName,
            bool isVideoChanged,
            IEnumerable<CandidateIndustry> industries,
            IEnumerable<CandidateDesiredSkill> desiredSkills,
            IEnumerable<CandidateSkill> skills,
            IEnumerable<CandidateLanguage> languages,
            IEnumerable<CandidateHobby> hobbies,
            ICollection<FormatType> formats,
            ICollection<WorkType> workTypes)
        {
            var expectedPhoneNumber = $"{phoneCountryCode}{phoneNumber}";

            Assert.Equal(email, candidate.Email?.Address);
            Assert.Equal(firstName, candidate.FirstName);
            Assert.Equal(lastName, candidate.LastName);
            Assert.Equal(currentPosition?.Id, candidate.CurrentPosition?.Id);
            Assert.Equal(currentPosition?.Code, candidate.CurrentPosition?.Code);
            Assert.Equal(birthDate, candidate.BirthDate);
            Assert.Equal(openForOpportunities, candidate.OpenForOpportunities);
            Assert.Equal(linkedInUrl, candidate.LinkedInUrl);
            Assert.Equal(personalWebsiteUrl, candidate.PersonalWebsiteUrl);
            Assert.Equal(addressLine, candidate.Address?.AddressLine);
            Assert.Equal(city, candidate.Address?.City);
            Assert.Equal(country, candidate.Address?.Country);
            Assert.Equal(postalCode, candidate.Address?.PostalCode);
            Assert.Equal(longitude, candidate.Address?.Coordinates?.Longitude);
            Assert.Equal(latitude, candidate.Address?.Coordinates?.Latitude);
            Assert.Equal(weeklyWorkHours, candidate.Terms?.PartTimeWorkingHours?.Weekly);
            Assert.Equal(currency, candidate.Terms?.Currency);
            Assert.Equal(freelanceHourlySalary, candidate.Terms?.Freelance?.HourlySalary);
            Assert.Equal(freelanceMonthlySalary, candidate.Terms?.Freelance?.MonthlySalary);
            Assert.Equal(permanentMonthlySalary, candidate.Terms?.Permanent?.MonthlySalary);
            Assert.Equal(startDate, candidate.Terms?.Availability?.From);
            Assert.Equal(endDate, candidate.Terms?.Availability?.To);

            if (workingHourTypes.Contains(WorkingHoursType.FullTime))
            {
                Assert.NotNull(candidate.Terms?.FulTimeWorkingHours);
            }

            if (workingHourTypes.Contains(WorkingHoursType.PartTime))
            {
                Assert.NotNull(candidate.Terms?.PartTimeWorkingHours);
            }

            if (workingHourTypes.Contains(WorkingHoursType.ProjectEmployment))
            {
                Assert.NotNull(candidate.Terms?.ProjectWorkingHours);
            }

            Assert.Equal(phoneCountryCode, candidate.Phone?.CountryCode);
            Assert.Equal(phoneNumber, candidate.Phone?.Number);
            Assert.Equal(expectedPhoneNumber, candidate.Phone?.PhoneNumber);
            Assert.Equal(yearsOfExperience, candidate.YearsOfExperience);
            Assert.Equal(bio, candidate.Bio);
            if (isCurriculumVitaeChanged)
            {
                Assert.Equal(curriculumVitaeUri, candidate.CurriculumVitae?.Uri);
                Assert.Equal(curriculumVitaeFileName, candidate.CurriculumVitae?.FileName);
            }
            if (isVideoChanged)
            {
                Assert.Equal(videoUri, candidate.Video?.Uri);
                Assert.Equal(videoFileName, candidate.Video?.FileName);
            }
            Assert.Equal(industries.Count(), candidate.Industries.Count);
            Assert.Equal(desiredSkills.Count(), candidate.DesiredSkills.Count);
            Assert.Equal(skills.Count(), candidate.Skills.Count);
            Assert.Equal(languages.Count(), candidate.Languages.Count);
            Assert.Equal(hobbies.Count(), candidate.Hobbies.Count);

            if (formats.Contains(FormatType.Onsite))
            {
                Assert.True(candidate.Terms?.Formats?.IsOnSite);
            }
            if (formats.Contains(FormatType.Hybrid))
            {
                Assert.True(candidate.Terms?.Formats?.IsHybrid);
            }
            if (formats.Contains(FormatType.Remote))
            {
                Assert.True(candidate.Terms?.Formats?.IsRemote);
            }

            if (workTypes.Contains(WorkType.Freelance))
            {
                Assert.NotNull(candidate.Terms?.Freelance);
            }
            if (workTypes.Contains(WorkType.Permanent))
            {
                Assert.NotNull(candidate.Terms?.Permanent);
            }
        }
    }
}
