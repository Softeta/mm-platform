using Contracts.Shared.Responses;

namespace Contracts.Job
{
    public class InterestedCandidate
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ImageResponse? Picture { get; set; }
        public string? Position { get; set; }
    }
}
