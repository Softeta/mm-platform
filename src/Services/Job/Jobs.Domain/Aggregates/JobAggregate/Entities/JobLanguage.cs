using Domain.Seedwork.Shared.Entities;
using Domain.Seedwork.Shared.ValueObjects;

namespace Jobs.Domain.Aggregates.JobAggregate.Entities
{
    public class JobLanguage : LanguageBase
    {
        public Guid JobId { get; private set; }

        private JobLanguage() { }

        public JobLanguage(Guid jobId, Guid languageId, string languageCode, string languageName)
        {
            Id = Guid.NewGuid();
            JobId = jobId;
            Language = new Language(languageId, languageCode, languageName);
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
