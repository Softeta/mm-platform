using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace BackOffice.Bff.API.Models.Job.Responses
{
    public class GetSelectedCandidateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public PhoneResponse? Phone { get; set; }
        public Position? CurrentPosition { get; set; }
        public ImageResponse? Picture { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
