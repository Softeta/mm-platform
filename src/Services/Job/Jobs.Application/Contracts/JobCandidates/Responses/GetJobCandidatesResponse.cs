using Contracts.Job.ArchivedCandidates.Responses;
using Contracts.Job.SelectedCandidates.Responses;
using Contracts.Shared;
using Contracts.Shared.Responses;
using Aggregate = Jobs.Domain.Aggregates.JobCandidatesAggregate;
using Common = Contracts.Job.JobCandidates.Responses;

namespace Jobs.Application.Contracts.JobCandidates.Responses
{
    public class GetJobCandidatesResponse : Common.GetJobCandidatesResponse
    {
        public static Common.GetJobCandidatesResponse FromDomain(Aggregate.JobCandidates jobCandidates)
        {
            return new Common.GetJobCandidatesResponse
            {
                Stage = jobCandidates.Stage,
                ShortListActivatedAt = jobCandidates.ShortListActivatedAt,
                SelectedCandidates = jobCandidates.SelectedCandidates.Select(GetJobSelectedCandidateResponse.FromDomain),
                ArchivedCandidates = jobCandidates.ArchivedCandidates.Select(a =>
                    new JobArchivedCandidateResponse
                    {
                        Id = a.CandidateId,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        PhoneNumber = a.PhoneNumber,
                        Position = a.Position != null
                        ? new Position
                        {
                            Id = a.Position.Id,
                            Code = a.Position.Code,
                        }
                        : null,
                        Picture = !string.IsNullOrEmpty(a.PictureUri)
                        ? new ImageResponse
                        {
                            Uri = a.PictureUri
                        }
                        : null,
                        Stage = a.Stage,
                        Brief = a.Brief,
                        HasApplied = a.HasApplied
                    }
                ),
            };
        }
    }
}
