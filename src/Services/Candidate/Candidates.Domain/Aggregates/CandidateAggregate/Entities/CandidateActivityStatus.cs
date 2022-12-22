using Domain.Seedwork;
using Domain.Seedwork.Enums;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateActivityStatus : Entity
    {
        public Guid CandidateId { get; private set; }
        public ActivityStatus ActivityStatus { get; private set; }

        public CandidateActivityStatus(Guid candidateId, ActivityStatus activityStatus)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
            Create(candidateId, activityStatus);
        }

        public CandidateActivityStatus()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTimeOffset.UtcNow;
        }

        public void Create(
            Guid candidateId,
            ActivityStatus activityStatus)
        {
            CandidateId = candidateId;
            ActivityStatus = activityStatus;
        }

        public void Update(ActivityStatus activityStatus)
        {
            ActivityStatus = activityStatus;
        }
    }
}
