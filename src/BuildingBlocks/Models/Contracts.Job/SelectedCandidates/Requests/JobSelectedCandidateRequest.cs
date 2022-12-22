using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Job.SelectedCandidates.Requests
{
    public class JobSelectedCandidateRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Position? Position { get; set; }
        public ImageResponse? Picture { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }

    public class JobSelectedCandidateExtendedRequest
    {
        public JobSelectedCandidateRequest SelectedCandidate { get; set; } = null!;
        public bool IsShortListedInOtherJob { get; set; }
        public bool isHiredInOtherJob { get; set; }
    }
}
