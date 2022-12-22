using BackOffice.Bff.API.Models.Job.Responses;

namespace BackOffice.Bff.API.Models.Job.ServiceRequests
{
    public class AddSelectedCandidatesServiceRequest
    {
        public IEnumerable<AddSelectedCandidateServiceRequest> SelectedCandidates { get; set; } = new List<AddSelectedCandidateServiceRequest>();

        public static AddSelectedCandidatesServiceRequest ToServiceRequest(List<GetSelectedCandidateResponse> candidates)
        {
            return new AddSelectedCandidatesServiceRequest
            {
                SelectedCandidates = candidates.Select(AddSelectedCandidateServiceRequest.ToServiceRequest)
            };
        }
    }
}
