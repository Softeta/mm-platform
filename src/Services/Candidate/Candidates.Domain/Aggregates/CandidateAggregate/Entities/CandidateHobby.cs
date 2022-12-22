using Domain.Seedwork.Shared.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateHobby : HobbyBase
    {
        public Guid CandidateId { get; private set; }

        public CandidateHobby(Guid hobbyId, Guid candidateId, string code)
        {
            Id = Guid.NewGuid();
            HobbyId = hobbyId;
            CandidateId = candidateId;
            Code = code;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
