using Domain.Seedwork.Enums;
using EventBus.EventHandlers;
using Jobs.Application.IntegrationEventHandlers.Publishers.Payloads;

namespace Jobs.Application.IntegrationEventHandlers.Publishers
{
    public class JobCandidateShortlistActivatedViaEmailIntegrationEvent : IntegrationEvent
    {
        public JobCandidateShortlistActivatedViaEmailIntegrationEvent(
            JobCandidatesPayload jobCandidates,
            string contactEmail,
            string? contactFirstName,
            SystemLanguage? contactSystemLanguage,
            Guid? contactExternalId,
            DateTimeOffset emittedAt) 
            : base(emittedAt)
        {
            Payload = new JobCandidatesShortlistActivatedViaEmailPayload
            {
                JobCandidates = jobCandidates,
                ContactEmail = contactEmail,
                ContactFirstName = contactFirstName,
                ContactSystemLanguage = contactSystemLanguage,
                ContactExternalId = contactExternalId
            };
        }

        public JobCandidatesShortlistActivatedViaEmailPayload Payload { get; }
    }
}
