namespace Contracts.Candidate.Courses.Requests
{
    public abstract class CandidateCourseRequestBase
    {
        public string Name { get; set; } = null!;

        public string IssuingOrganization { get; set; } = null!;

        public string? Description { get; set; }

    }
}
