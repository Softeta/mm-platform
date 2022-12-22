using Domain.Seedwork.Shared.Entities;
using Domain.Seedwork.Shared.ValueObjects;

namespace Candidates.Domain.Aggregates.CandidateAggregate.Entities
{
    public class CandidateLanguage : LanguageBase
    {
        public Guid CandidateId { get; private set; }

        private CandidateLanguage() { }

        public CandidateLanguage(Guid candidateId, Guid languageId, string languageCode, string languageName)
        {
            Id = Guid.NewGuid();
            CandidateId = candidateId;
            Language = new Language(languageId, languageCode, languageName);
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
