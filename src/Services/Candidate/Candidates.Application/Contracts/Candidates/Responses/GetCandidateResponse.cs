using Candidates.Application.Contracts.Courses.Responses;
using Candidates.Application.Contracts.Educations.Responses;
using Candidates.Application.Contracts.WorkExperiences.Responses;
using Candidates.Domain.Aggregates.CandidateAggregate;
using Contracts.Shared;
using Contracts.Shared.Helpers;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;
using Persistence.Customization.FileStorage.Clients.Private;
using Common = Contracts.Candidate.Candidates.Responses;
using Local = Candidates.Application.Contracts;

namespace Candidates.Application.Contracts.Candidates.Responses
{
    public class GetCandidateResponse : Common.GetCandidateResponse
    {
        public static Common.GetCandidateResponse FromDomain(
            Candidate candidate,
            IPrivateBlobClient privateBlobClient)
        {
            return new Common.GetCandidateResponse
            {
                Id = candidate.Id,
                Status = candidate.Status,
                Email = candidate.Email?.Address,
                IsEmailVerified = candidate.Email?.IsVerified,
                FullName = FullNameHelper.GetFullName(candidate.FirstName, candidate.LastName),
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Picture = ImageResponse.FromDomain(candidate.Picture),
                CurrentPosition = Position.FromDomain(candidate.CurrentPosition),
                BirthDate = candidate.BirthDate,
                OpenForOpportunities = candidate.OpenForOpportunities,
                LinkedInUrl = candidate.LinkedInUrl,
                PersonalWebsiteUrl = candidate.PersonalWebsiteUrl,
                YearsOfExperience = candidate.YearsOfExperience,
                ActivityStatuses = candidate.ActivityStatuses.Select(x => x.ActivityStatus).ToList(),
                Currency = candidate.Terms?.Currency,
                Address = AddressWithLocation.FromDomain(candidate.Address),
                Freelance = Local.Freelance.FromDomain(candidate.Terms?.Freelance),
                Permanent = Local.Permanent.FromDomain(candidate.Terms?.Permanent),
                SystemLanguage = candidate.SystemLanguage,
                TermsAndConditions = LegalInformationAgreement.FromDomain(candidate.TermsAndConditions),
                MarketingAcknowledgement = LegalInformationAgreement.FromDomain(candidate.MarketingAcknowledgement),
                Bio = candidate.Bio,
                CurriculumVitae = DocumentResponse.FromDomain(candidate.CurriculumVitae, privateBlobClient),
                Video = DocumentResponse.FromDomain(candidate.Video, privateBlobClient),
                StartDate = candidate.Terms?.Availability?.From,
                EndDate = candidate.Terms?.Availability?.To,
                WeeklyWorkHours = candidate.Terms?.PartTimeWorkingHours?.Weekly,
                Phone = PhoneFullResponse.FromDomain(candidate.Phone),
                IsShortlisted = candidate.IsShortListed,
                Note = NoteResponse.FromDomain(candidate.Note),
                Languages = candidate.Languages.Select(Language.FromDomain),
                Skills = candidate.Skills.Select(Skill.FromDomain),
                DesiredSkills = candidate.DesiredSkills.Select(Skill.FromDomain),
                Industries = candidate.Industries.Select(Industry.FromDomain),
                WorkTypes = GetWorkTypes(candidate),
                WorkingHourTypes = GetWorkingHoursTypes(candidate),
                Formats = GetFormatTypes(candidate),
                CandidateCourses = candidate.Courses.Select(x => CandidateCourseResponse.FromDomain(x, privateBlobClient)),
                CandidateEducations = candidate.Educations.Select(x => CandidateEducationResponse.FromDomain(x, privateBlobClient)),
                CandidateWorkExperiences = candidate.WorkExperiences.Select(CandidateWorkExperienceResponse.FromDomain),
                Hobbies = candidate.Hobbies.Select(Hobby.FromDomain)
            };
        }

        private static List<WorkType> GetWorkTypes(Candidate candidate)
        {
            var workTypes = new List<WorkType>();
            if (candidate.Terms?.Freelance?.WorkType != null)
            {
                workTypes.Add(WorkType.Freelance);
            }
            if (candidate.Terms?.Permanent?.WorkType != null)
            {
                workTypes.Add(WorkType.Permanent);
            }
            return workTypes;
        }

        private static List<WorkingHoursType> GetWorkingHoursTypes(Candidate candidate)
        {
            var workingHoursTypes = new List<WorkingHoursType>();
            if (candidate.Terms?.PartTimeWorkingHours != null)
            {
                workingHoursTypes.Add(WorkingHoursType.PartTime);
            }
            
            if (candidate.Terms?.FulTimeWorkingHours != null)
            {
                workingHoursTypes.Add(WorkingHoursType.FullTime);
            }
            
            if (candidate.Terms?.ProjectWorkingHours != null)
            {
                workingHoursTypes.Add(WorkingHoursType.ProjectEmployment);
            }

            return workingHoursTypes;
        }

        private static List<FormatType> GetFormatTypes(Candidate candidate)
        {
            var formatTypes = new List<FormatType>();
            if (candidate.Terms?.Formats is null)
            {
                return formatTypes;
            }

            if (candidate.Terms.Formats.IsOnSite)
            {
                formatTypes.Add(FormatType.Onsite);
            }
            if (candidate.Terms.Formats.IsHybrid)
            {
                formatTypes.Add(FormatType.Hybrid);
            }
            if (candidate.Terms.Formats.IsRemote)
            {
                formatTypes.Add(FormatType.Remote);
            }

            return formatTypes;
        }
    }
}
