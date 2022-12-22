using Contracts.Shared.Responses;
using Jobs.Domain.Aggregates.JobAggregate.Entities;
using Common = Contracts.Job;

namespace Jobs.Application.Contracts
{
    internal class InterestedCandidate : Common.InterestedCandidate
    {
        public static InterestedCandidate FromDomain(JobInterestedCandidate interestedCandidate)
        {
            return new InterestedCandidate
            {
                Id = interestedCandidate.CandidateId,
                FirstName = interestedCandidate.FirstName,
                LastName = interestedCandidate.LastName,
                Position = interestedCandidate.Position,
                Picture = new ImageResponse { Uri = interestedCandidate.PictureUri}
            };
        }
    }
}
