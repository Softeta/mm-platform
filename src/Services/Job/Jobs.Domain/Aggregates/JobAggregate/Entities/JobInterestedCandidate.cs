using Domain.Seedwork;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobInterestedCandidate : Entity
    {
        public Guid JobId { get; private set; }
        public Guid CandidateId { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public string? Position { get; private set; }
        public string? PictureUri { get; private set; }

        public JobInterestedCandidate(
            Guid jobId,
            Guid candidateId,
            string firstName, 
            string lastName,
            string? position,
            string? pictureUri)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            CandidateId = candidateId;
            FirstName = firstName;
            LastName = lastName;
            Position = position;
            PictureUri = pictureUri;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
