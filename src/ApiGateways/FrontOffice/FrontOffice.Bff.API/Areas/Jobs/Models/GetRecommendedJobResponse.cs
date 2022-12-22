using Common = Contracts.Job.Jobs.Responses;

namespace FrontOffice.Bff.API.Areas.Jobs.Models
{
    public class GetRecommendedJobResponse : Common.GetJobBriefResponse
    {
        public double Score { get; set; }
    }
}
