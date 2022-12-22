using BackOffice.Bff.API.Models.Job.Responses;
using Contracts.Job;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class InterestedCandidateServiceRequest : InterestedCandidate
    {
        public static InterestedCandidateServiceRequest ToServiceRequest(GetInterestedCandidateResponse candidate)
        {
            return new InterestedCandidateServiceRequest
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Picture = candidate.Picture,
                Position = candidate.CurrentPosition?.Code,
            };
        }
    }
}
