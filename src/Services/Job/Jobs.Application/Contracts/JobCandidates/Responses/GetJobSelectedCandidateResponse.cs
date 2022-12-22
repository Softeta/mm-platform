using Contracts.Shared;
using Contracts.Shared.Responses;
using Jobs.Domain.Aggregates.JobCandidatesAggregate.Entities;
using Common = Contracts.Job.SelectedCandidates.Responses;

namespace Jobs.Application.Contracts.JobCandidates.Responses
{
    public class GetJobSelectedCandidateResponse : Common.JobSelectedCandidateResponse
    {
        public static Common.JobSelectedCandidateResponse FromDomain(JobSelectedCandidate jobSelectedCandidate)
        {
            return new Common.JobSelectedCandidateResponse
            {
                Id = jobSelectedCandidate.CandidateId,
                CandidateId = jobSelectedCandidate.CandidateId,
                FirstName = jobSelectedCandidate.FirstName,
                LastName = jobSelectedCandidate.LastName,
                Email = jobSelectedCandidate.Email,
                PhoneNumber = jobSelectedCandidate.PhoneNumber,
                Position = jobSelectedCandidate.Position != null
                        ? new Position
                        {
                            Id = jobSelectedCandidate.Position.Id,
                            Code = jobSelectedCandidate.Position.Code,
                        }
                        : null,
                Picture = !string.IsNullOrEmpty(jobSelectedCandidate.PictureUri)
                        ? new ImageResponse
                        {
                            Uri = jobSelectedCandidate.PictureUri
                        }
                        : null,
                Stage = jobSelectedCandidate.Stage,
                Ranking = jobSelectedCandidate.Ranking,
                IsShortListed = jobSelectedCandidate.IsShortListed,
                IsShortListedInOtherJob = jobSelectedCandidate.IsShortListedInOtherJob,
                IsHiredInOtherJob = jobSelectedCandidate.IsHiredInOtherJob,
                IsHired = jobSelectedCandidate.IsHired,
                Brief = jobSelectedCandidate.Brief,
                IsInvited = jobSelectedCandidate.InvitedAt.HasValue,
                HasApplied = jobSelectedCandidate.HasApplied
            };
        }
    }
}
