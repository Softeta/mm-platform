using Domain.Seedwork.Shared.Entities;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateIndustry : IndustryBase
    {
        public Guid CandidateId { get; private set; }

        public CandidateIndustry(Guid industryId, Guid candidateId, string code)
        {
            Id = Guid.NewGuid();
            IndustryId = industryId;
            CandidateId = candidateId;
            Code = code;
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
