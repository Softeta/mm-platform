using Domain.Seedwork.Enums;
using Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload.Models;

namespace Jobs.Application.IntegrationEventHandlers.Subscribers.Candidates.Payload
{
    public class CandidatePayload
    {
        public Guid Id { get; set; }
        public CandidateStatus Status { get; set; }
        public Email? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PictureUri { get; set; }
        public Position? CurrentPosition { get; set; }
        public Phone? Phone { get; set; }
        public Image? Picture { get; set; }
        public SystemLanguage? SystemLanguage { get; set; }
    }
}
