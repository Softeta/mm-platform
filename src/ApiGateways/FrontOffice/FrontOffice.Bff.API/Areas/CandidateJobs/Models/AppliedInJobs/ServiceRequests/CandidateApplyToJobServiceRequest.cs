using Contracts.Job.Jobs.Responses;
using Domain.Seedwork.Enums;
using Common = Contracts.Candidate.CandidateJobs;

namespace FrontOffice.Bff.API.Areas.AppliedInJobs.Models.SelectedInJobs.ServiceRequests
{
    public class CandidateApplyToJobServiceRequest : Common.Requests.CandidateApplyToJobRequest
    {
        public static CandidateApplyToJobServiceRequest ToServiceRequest(GetJobResponse job)
        {
            return new CandidateApplyToJobServiceRequest
            {
                JobStage = job.Stage,
                PositionId = job.Position.Id,
                PositionCode = job.Position.Code,
                PositionAliasToId = job.Position.AliasTo?.Id,
                PositionAliasToCode = job.Position.AliasTo?.Code,
                CompanyId = job.Company.Id,
                CompanyName = job.Company.Name,
                CompanyLogo = job.Company.Logo?.Uri,
                Freelance = job.WorkTypes.Contains(WorkType.Freelance) ? WorkType.Freelance : null,
                Permanent = job.WorkTypes.Contains(WorkType.Permanent) ? WorkType.Permanent : null,
                StartDate = job.StartDate,
                DeadlineDate = job.DeadLineDate
            };
        }
    }
}
