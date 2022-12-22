using BackOffice.Bff.API.Models.Job.Responses;
using Contracts.Job.SelectedCandidates.Requests;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class AddSelectedCandidateServiceRequest : JobSelectedCandidateRequest
    {
        public static AddSelectedCandidateServiceRequest ToServiceRequest(GetSelectedCandidateResponse candidate)
        {
            return new AddSelectedCandidateServiceRequest
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.Phone?.PhoneNumber,
                Position = candidate.CurrentPosition,
                Picture = candidate.Picture,
                SystemLanguage = candidate.SystemLanguage
            };
        }
    }
}
