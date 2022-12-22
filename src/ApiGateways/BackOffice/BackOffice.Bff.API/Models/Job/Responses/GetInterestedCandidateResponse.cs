using Contracts.Shared;
using Contracts.Shared.Responses;

namespace BackOffice.Bff.API.Models.Job.Responses
{
    public class GetInterestedCandidateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ImageResponse? Picture { get; set; }
        public Position? CurrentPosition { get; set; }
    }
}
