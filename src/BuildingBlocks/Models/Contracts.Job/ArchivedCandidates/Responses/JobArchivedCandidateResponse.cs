using Contracts.Shared;
using Contracts.Shared.Responses;
using Domain.Seedwork.Enums;

namespace Contracts.Job.ArchivedCandidates.Responses
{
    public class JobArchivedCandidateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public Position? Position { get; set; }
        public ImageResponse? Picture { get; set; }
        public ArchivedCandidateStage Stage { get; set; }
        public string? Brief { get; set; }
        public bool HasApplied { get; set; }
    }
}
