namespace Contracts.Job.Jobs.Responses
{
    public record GetJobsResponse(
        int Count,
        List<GetJobBriefResponse> Jobs);
}
