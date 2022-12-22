using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Job.SelectedCandidates.Responses
{
    public class JobSelectedCandidateResponse
    {
        public Guid Id { get; set; }
        public Guid CandidateId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Position? Position { get; set; }
        public ImageResponse? Picture { get; set; }
        public SelectedCandidateStage Stage { get; set; }
        public int? Ranking { get; set; }
        public bool IsShortListed { get; set; }
        public bool IsShortListedInOtherJob { get; set; }
        public bool IsHired { get; set; }
        public bool IsHiredInOtherJob { get; set; }
        public string? Brief { get; set; }
        public bool IsInvited { get; set; }
        public bool HasApplied { get; set; }
    }
}
