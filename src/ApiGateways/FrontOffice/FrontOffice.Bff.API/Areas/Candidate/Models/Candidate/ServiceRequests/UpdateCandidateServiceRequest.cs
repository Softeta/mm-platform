using Contracts.Shared.Requests;
using Common = Contracts.Candidate.Candidates;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.ServiceRequests
{
    public class UpdateCandidateServiceRequest : Common.Requests.UpdateCandidateRequest
    {
        public static Common.Requests.UpdateCandidateRequest ToServiceRequest(Common.Responses.GetCandidateResponse candidate)
        {
            return new Common.Requests.UpdateCandidateRequest
            {
                Email = candidate.Email,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                CurrentPosition = candidate.CurrentPosition,
                BirthDate = candidate.BirthDate,
                OpenForOpportunities = candidate.OpenForOpportunities,
                LinkedInUrl = candidate.LinkedInUrl,
                PersonalWebsiteUrl = candidate.PersonalWebsiteUrl,
                YearsOfExperience = candidate.YearsOfExperience,
                Address = candidate.Address,
                StartDate = candidate.StartDate,
                EndDate = candidate.EndDate,
                Currency = candidate.Currency,
                Freelance = candidate.Freelance,
                Permanent = candidate.Permanent,
                WeeklyWorkHours = candidate.WeeklyWorkHours,
                Phone = PhoneRequest.From(candidate.Phone?.CountryCode, candidate.Phone?.Number),
                ActivityStatuses = candidate.ActivityStatuses,
                Languages = candidate.Languages,
                Skills = candidate.Skills,
                DesiredSkills = candidate.DesiredSkills,
                Industries = candidate.Industries,
                WorkTypes = candidate.WorkTypes.ToList(),
                WorkingHourTypes = candidate.WorkingHourTypes.ToList(),
                Formats = candidate.Formats.ToList(),
                Hobbies = candidate.Hobbies,
                Bio = candidate.Bio,
            };
        }

        public static Common.Requests.UpdateCandidateRequest ToServiceRequestWithNewPicture(Common.Responses.GetCandidateResponse candidate, UpdateFileCacheRequest picture)
        {
            var payload = ToServiceRequest(candidate);
            payload.Picture = picture;

            return payload;
        }

        public static Common.Requests.UpdateCandidateRequest ToServiceRequestWithNewCurriculumVitae(Common.Responses.GetCandidateResponse candidate, UpdateFileCacheRequest file)
        {
            var payload = ToServiceRequest(candidate);
            payload.CurriculumVitae = file;

            return payload;
        }

        public static Common.Requests.UpdateCandidateRequest ToServiceRequestWithNewVideo(Common.Responses.GetCandidateResponse candidate, UpdateFileCacheRequest file)
        {
            var payload = ToServiceRequest(candidate);
            payload.Video = file;

            return payload;
        }
    }
}