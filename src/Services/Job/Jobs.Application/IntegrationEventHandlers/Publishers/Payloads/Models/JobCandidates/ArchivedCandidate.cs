using Domain.Seedwork.Enums;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;

namespace Jobs.Application.IntegrationEventHandlers.Publishers.Payloads.Models.JobCandidates
{
    public class ArchivedCandidate : Candidate
    {
        public ArchivedCandidateStage Stage { get; set; }

        public static ArchivedCandidate FromDomain(JobArchivedCandidate candidate)
        {
            return new ArchivedCandidate
            {
                CandidateId = candidate.CandidateId,
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                Email = candidate.Email,
                PhoneNumber = candidate.PhoneNumber,
                PictureUri = candidate.PictureUri,
                Stage = candidate.Stage,
                InvitedAt = candidate.InvitedAt,
                SystemLanguage = candidate.SystemLanguage,
                HasApplied = candidate.HasApplied
            };
        }
    }
}
