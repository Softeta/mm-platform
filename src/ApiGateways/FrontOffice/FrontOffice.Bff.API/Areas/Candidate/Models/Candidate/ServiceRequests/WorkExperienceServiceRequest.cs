using FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.Requests;
using Common = Contracts.Candidate.WorkExperiences;

namespace FrontOffice.Bff.API.Areas.Candidate.Models.Candidate.ServiceRequests
{
    public class WorkExperienceServiceRequest : Common.Requests.CandidateWorkExperienceRequest
    {
        public static Common.Requests.CandidateWorkExperienceRequest ToServiceRequest(UpdateWorkExperienceRequest workExperience)
        {
            return new Common.Requests.CandidateWorkExperienceRequest
            {
                Type = workExperience.Type,
                CompanyName = workExperience.CompanyName,
                Position = workExperience.Position,
                From = workExperience.From,
                To = workExperience.To,
                JobDescription = workExperience.JobDescription,
                IsCurrentJob = workExperience.IsCurrentJob,
                Skills = workExperience.Skills
            };
        }
    }
}
