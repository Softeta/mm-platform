using Domain.Seedwork.Enums;
using EventBus.Constants;
using Jobs.Domain.Aggregates.JobCandidatesAggregate;

namespace Jobs.Domain.Events.JobCandidatesAggregate
{
    public class JobCandidatesSharedShortlistViaEmailDomainEvent : JobCandidatesChangedDomainEvent
    {
        public JobCandidatesSharedShortlistViaEmailDomainEvent(
            JobCandidates jobCandidates, 
            string contactEmail,
            string? contactFirstName,
            SystemLanguage? contactSystemLanguage,
            Guid? contactExternalId,
            DateTimeOffset emittedAt) :
            base(jobCandidates, emittedAt, Topics.JobCandidatesChanged.Filters.JobCandidatesSharedShortlistViaEmail)
        {
            ContactEmail = contactEmail;
            ContactFirstName = contactFirstName;
            ContactSystemLanguage = contactSystemLanguage;
            ContactExternalId = contactExternalId;
        }

        public string ContactEmail { get; set; }
        public string? ContactFirstName { get; set; }
        public SystemLanguage? ContactSystemLanguage { get; set; }
        public Guid? ContactExternalId { get; set; }
    }
}
