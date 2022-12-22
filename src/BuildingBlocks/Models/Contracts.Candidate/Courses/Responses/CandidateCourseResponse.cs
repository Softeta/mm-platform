using Contracts.Shared.Responses;

namespace Contracts.Candidate.Courses.Responses
{
    public class CandidateCourseResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string IssuingOrganization { get; set; } = null!;
        public string? Description { get; set; }
        public DocumentResponse? Certificate { get; set; }
    }
}
