using Contracts.Shared.Responses;

namespace Contracts.Candidate.Educations.Responses
{
    public class CandidateEducationResponse
    {
        public Guid Id { get; set; }
        public string SchoolName { get; set; } = null!;
        public string Degree { get; set; } = null!;
        public string FieldOfStudy { get; set; } = null!;
        public DateTimeOffset From { get; set; }
        public DateTimeOffset? To { get; set; }
        public string? StudiesDescription { get; set; }
        public bool IsStillStudying { get; set; }
        public DocumentResponse? Certificate { get; set; }
    }
}
