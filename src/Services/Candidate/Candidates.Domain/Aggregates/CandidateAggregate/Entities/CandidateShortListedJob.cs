using Domain.Seedwork;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateShortListedJob : Entity
    {
        public Guid CandidateId { get; private set; }
        public Guid JobId { get; private set; }

        public CandidateShortListedJob(Guid candidateId, Guid jobId)
        {
            Id = Guid.NewGuid();
            CandidateId = candidateId;
            JobId = jobId;
            CreatedAt = DateTimeOffset.Now;
        }
    }
}
