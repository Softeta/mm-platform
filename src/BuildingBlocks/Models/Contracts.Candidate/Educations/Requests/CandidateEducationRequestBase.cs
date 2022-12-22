namespace Contracts.Candidate.Educations.Requests
{
    public abstract class CandidateEducationRequestBase
    {
        public string SchoolName { get; set; } = null!;

        public string Degree { get; set; } = null!;

        public string FieldOfStudy { get; set; } = null!;

        public DateTimeOffset From { get; set; }

        public DateTimeOffset? To { get; set; }

        public string? StudiesDescription { get; set; }

        public bool IsStillStudying { get; set; }
    }
}
