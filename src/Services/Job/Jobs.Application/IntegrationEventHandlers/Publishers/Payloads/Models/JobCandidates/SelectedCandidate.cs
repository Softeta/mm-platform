using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates
{
    public class SelectedCandidate : Candidate
    {
        public SelectedCandidateStage Stage { get; set; }
        public DateTimeOffset? ShortListSendDate { get; set; }
        public bool IsShortListed { get; set; }

        public static SelectedCandidate FromDomain(JobSelectedCandidate candidate)
        {
            return new SelectedCandidate
            {
                CandidateId = candidate.CandidateId,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                PictureUri = candidate.PictureUri,
                Stage = candidate.Stage,
                IsShortListed = candidate.IsShortListed,
                InvitedAt =  candidate.InvitedAt,
                SystemLanguage = candidate.SystemLanguage,
                HasApplied = candidate.HasApplied
            };
        }
    }
}
